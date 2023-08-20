namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Author;
using Services.Interfaces;
using Client.ViewModels.Category;
using Infrastructure.Extensions;

using static Common.ErrorMessagesConstants;
using static Common.NotificationMessagesConstants;

[Authorize]
public class AuthorController : Controller
{
    private const string entityName = "author";

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

        queryModel.Authors = filteredAuthors.Authors;
        queryModel.Categories = categories.Select(c => c.Name);
        queryModel.TotalAutrhosCount = filteredAuthors.TotalAuthorsCount;

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        string userId = this.User.GetId()!;
        try
        {
            IEnumerable<AuthorViewModel> myAuthors = null;

            myAuthors = await _authorService.AllAuthorsByUserId(userId);

            return View(myAuthors);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "load your authors");

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> MyPublishings()
    {
        string userId = this.User.GetId();
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), "Publisher");
        }

        string publisherId = (await _publisherService.GetPublisherAsync(userId)).Id.ToString();
        var model = await _authorService.AllAuthorsByPublisherId(userId, publisherId);

        return View(model);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            var authorModel = await _authorService.GetAuthorDetailsAsync(id, this.User.GetId()!);

            return View(authorModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "get the author");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var isPublisher = await _publisherService.ExistsById(User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

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
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "load the page");

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
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), "Publisher");
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(newAuthor.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(newAuthor.CategoryId), String.Format(NoEntityFoundErrorMessage, "category"));
        }

        if (!ModelState.IsValid)
        {
            newAuthor.Categories = await _categoryService.GetAllAsync();

            return View(newAuthor);
        }

        var publisher = await _publisherService.GetPublisherAsync(userId);

        string authorId;
        try
        {
            authorId = await _authorService.CreateAuthor(newAuthor, publisher);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "add a new author");
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
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

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

            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "get the author");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, AuthorFormModel author)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(Mine));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(Mine));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(author.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(author.CategoryId), String.Format(NoEntityFoundErrorMessage, "category"));
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
    public async Task<IActionResult> Activate(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction(nameof(Mine));
        }

        var author = await _authorService.GetAuthorAsync(id);

        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Activate(AuthorDetailsViewModel author)
    {
        bool exists = await _authorService.Exists(author.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(author.Id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(Mine));
        }

        await _authorService.ActivateAsync(author.Id);

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Disable(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction(nameof(Mine));
        }

        var author = await _authorService.GetAuthorAsync(id);

        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Disable(AuthorDetailsViewModel author)
    {
        bool exists = await _authorService.Exists(author.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(author.Id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(Mine));
        }

        await _authorService.DisableAsync(author.Id);

        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    public async Task<IActionResult> Follow(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        bool isFollowing = await _authorService.IsFollowedByUserWithId(id, this.User.GetId()!);
        if (isFollowing)
        {
            TempData[ErrorMessage] = "You are already following this author.";

            return RedirectToAction(nameof(Mine));
        }

        try
        {
            await _authorService.FollowAsync(id, this.User.GetId()!);

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "update your followings list");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unfollow(string id)
    {
        var exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _authorService.UnfollowAsync(id, this.User.GetId()!);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "unfollow the author");
        }

        return RedirectToAction(nameof(Mine));
    }

    [HttpGet]
    public async Task<IActionResult> Subscribe(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        bool isPublisher = await _publisherService.ExistsById(this.User.GetId()!);
        if (isPublisher)
        {
            TempData[ErrorMessage] = PublishersCannotSubscribeErrorMessage;

            return RedirectToAction(nameof(All));
        }

        try
        {
            var authorModel = await _authorService.GetAuthorSubscribtionsAsync(id);

            return View(authorModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "create your subscription");

            return RedirectToAction(nameof(All));
        }

    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(AuthorSubscribeFormModel authorSubscriptionForm)
    {
        bool exists = await _authorService.Exists(authorSubscriptionForm.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
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
            TempData[ErrorMessage] = PublishersCannotSubscribeErrorMessage;

            return RedirectToAction(nameof(All));
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
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "create your subscription");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        var exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _authorService.UnsubscribeAsync(id, this.User.GetId()!);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "unsubscribe from the author");
        }

        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> BecomeConnectedPublisher(string id)
    {
        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            RedirectToAction(nameof(PublisherController.Become), "Publisher");
        }

        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (isConnectedPublisher)
        {
            TempData[ErrorMessage] = AlreadyAConnectedPublisherErrorMessage;
        }
        else
        {
            var publisher = await _publisherService.GetPublisherAsync(userId);
            await _authorService.AddPublisherAsync(id, publisher);
        }

        return RedirectToAction(nameof(MyPublishings));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveConnectedPublisher(string id)
    {
        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            RedirectToAction(nameof(PublisherController.Become), "Publisher");
        }

        bool isConnectedPublisher = await _authorService.IsConnectedPublisher(id, userId);
        if (!isConnectedPublisher)
        {
            TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;
        }
        else
        {
            var publisher = await _publisherService.GetPublisherAsync(userId);
            await _authorService.RemovePublisherAsync(id, publisher.Id);
        }

        return RedirectToAction(nameof(MyPublishings));
    }
}
