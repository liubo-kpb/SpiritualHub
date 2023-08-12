namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Author;
using Services.Interfaces;
using Infrastructure.Extensions;
using ViewModels.Category;

using static Common.NotificationMessagesConstants;

[Authorize]
public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;
    private readonly ISubscriptionService _subscriptionService;

    public AuthorController(IAuthorService authorService,
                            ICategoryService categoryService,
                            IPublisherService publisherService,
                            ISubscriptionService subscriptionService)
    {
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
        _subscriptionService = subscriptionService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllAuthorsQueryModel queryModel)
    {
        var filteredAuthors = await _authorService.GetAllAsync(queryModel, this.User.GetId()!);
        IEnumerable<CategoryServiceModel> categories = await _categoryService.GetAllAsync();

        queryModel = new AllAuthorsQueryModel()
        {
            Authors = filteredAuthors.Authors,
            Categories = categories.Select(c => c.Name)
        };

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        IEnumerable<AuthorViewModel> myAuthors = null;

        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsById(userId);

        try
        {
            if (isPublisher)
            {
                var publisherId = (await _publisherService.GetPublisher(userId)).Id.ToString();

                myAuthors = await _authorService.AllAuthorsByPublisherId(userId, publisherId);
            }
            else
            {
                myAuthors = await _authorService.AllAuthorsByUserId(userId);
            }

            return View(myAuthors);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to load your authors. Please try again later later! ";

            return RedirectToAction("Index", "Home");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        try
        {
            var authorModel = await _authorService.GetAuthorDetailsAsync(id, this.User.GetId()!);

            return View(authorModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to get the author. Please try again later later!";

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var isPublisher = await _publisherService.ExistsById(User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = "You need to become a publisher to be able to add more authors.";

            return RedirectToAction("Index", "Home");
        }

        try
        {
            return View(new AuthorFormModel
            {
                Categories = await _categoryService.GetAllAsync()
            });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to load the page. Please try again later later!";

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add(AuthorFormModel newAuthor)
    {
        var userId = User.GetId()!;
        var isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = "You need to become a publisher to be able to add more authors.";

            return RedirectToAction(nameof(PublisherController.Become), "Publisher");
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(newAuthor.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(newAuthor.CategoryId), "Category doesn't exist. Please select a valid category!");
        }

        if (!ModelState.IsValid)
        {
            newAuthor.Categories = await _categoryService.GetAllAsync();

            return View(newAuthor);
        }

        var publisher = await _publisherService.GetPublisher(userId);

        string authorId;
        try
        {
            authorId = await _authorService.CreateAuthor(newAuthor, publisher);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to add a new author. Please try again later later!";
            newAuthor.Categories = await _categoryService.GetAllAsync();

            return View(newAuthor);
        }

        return RedirectToAction(nameof(Details), new { id = authorId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.HasConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = "You need to be a publisher of this author to be able to make changes.";

            return RedirectToAction(nameof(Mine));
        }

        try
        {
            var author = await _authorService.GetAuthorAsync(id);
            author.Categories = await _categoryService.GetAllAsync();

            return View(author);
        }
        catch (Exception)
        {

            TempData[ErrorMessage] = "An unexpected error occurred when attempting to get the author. Please try again later later!";

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, AuthorFormModel author)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction(nameof(Mine));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.HasConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = "You need to be a publisher of this author to be able to make changes.";

            return RedirectToAction(nameof(Mine));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(author.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(author.CategoryId), "Category doesn't exist. Please select a valid category!");
        }

        if (!ModelState.IsValid)
        {
            author.Categories = await _categoryService.GetAllAsync();

            return View(author);
        }

        await _authorService.Edit(author);

        return RedirectToAction(nameof(Details), new { id = author.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Disable(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.HasConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = "You need to be a publisher of this author to be able to make changes.";

            return RedirectToAction(nameof(Mine));
        }

        var author = await _authorService.GetAuthorAsync(id);

        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Disable(AuthorDetailsViewModel author)
    {
        string authorId = author.Id.ToString();
        bool exists = await _authorService.Exists(authorId);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.HasConnectedPublisher(authorId, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = "You need to be a publisher of this author to be able to make changes.";

            return RedirectToAction(nameof(Mine));
        }

        await _authorService.DisableAsync(authorId);

        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    public async Task<IActionResult> Follow(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction(nameof(All));
        }

        bool isFollowing = await _authorService.IsFollowedByUserWithId(id, this.User.GetId()!);
        if (isFollowing)
        {
            TempData[ErrorMessage] = "You are already following this author.";

            return RedirectToAction(nameof(Mine));
        }

        await _authorService.FollowAsync(id, this.User.GetId()!);

        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> Unfollow(string id)
    {
        return RedirectToAction(nameof(Mine));
    }

    [HttpGet]
    public async Task<IActionResult> Subscribe(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        bool isPublisher = await _publisherService.ExistsById(this.User.GetId()!);
        if (isPublisher)
        {
            TempData[ErrorMessage] = "Publishers cannot subscribe to authors.";

            return RedirectToAction(nameof(All));
        }

        try
        {
            var authorModel = await _authorService.GetAuthorSubscribtionsAsync(id);

            return View(authorModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred wthen attempting to create your subscription. Please try again later!";

            return RedirectToAction(nameof(All));
        }

    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(AuthorSubscribeFormModel authorSubscriptionForm)
    {
        bool exists = await _authorService.Exists(authorSubscriptionForm.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again later!";

            return RedirectToAction("All");
        }

        bool isExistingSubscription = await _subscriptionService.ExistsByIdAsync(authorSubscriptionForm.SubscriptionId);
        if (!isExistingSubscription)
        {
            TempData[ErrorMessage] = "Please select a valid subscription plan!";

            return View(await _authorService.GetAuthorSubscribtionsAsync(authorSubscriptionForm.Id));
        }

        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (isPublisher)
        {
            TempData[ErrorMessage] = "Publishers cannot subscribe to authors.";

            return RedirectToAction("All");
        }

        try
        {
            await _authorService.SubscribeAsync(authorSubscriptionForm.Id, authorSubscriptionForm.SubscriptionId, userId);

            return RedirectToAction(nameof(Mine));
        }
        catch (ArgumentException e)
        {
            TempData[ErrorMessage] = e.Message;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to create your subscription. Please try again later later!";

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        return RedirectToAction(nameof(Mine));
    }
}
