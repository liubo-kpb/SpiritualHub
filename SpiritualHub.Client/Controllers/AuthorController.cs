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

    public AuthorController(IAuthorService authorService, ICategoryService categoryService, IPublisherService publisherService)
    {
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllAuthorsQueryModel queryModel)
    {
        var filteredAuthors = await _authorService.GetAllAsync(queryModel);
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

        string userId = this.User.GetId();
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (isPublisher)
        {
            var publisherId = (await _publisherService.GetPublisher(userId)).Id.ToString();

            myAuthors = await _authorService.AllAuthorsByPublisherId(publisherId);
        }
        else
        {
            myAuthors = await _authorService.AllAuthorsByUserId(userId);
        }

        return View(myAuthors);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again!";

            return RedirectToAction("All");
        }

        var authorModel = await _authorService.GetAuthorDetailsAsync(id);

        return View(authorModel);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var userId = User.GetId();
        var isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = "You need to become a publisher to be able to add more authors.";

            return RedirectToAction("Index", "Home");
        }

        return View(new AuthorFormModel
        {
            Categories = await _categoryService.GetAllAsync()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(AuthorFormModel newAuthor)
    {
        var userId = User.GetId();
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
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to add a new author. Please try again later!";
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
            TempData[ErrorMessage] = "No such author found. Please try again!";

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
        author.Categories = await _categoryService.GetAllAsync();

        return View(author);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, AuthorFormModel author)
    {
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            TempData[ErrorMessage] = "No such author found. Please try again!";

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
            TempData[ErrorMessage] = "No such author found. Please try again!";

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
            TempData[ErrorMessage] = "No such author found. Please try again!";

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
    public async Task<IActionResult> Follow(Guid id)
    {
        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> Unfollow(Guid id)
    {
        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(Guid id)
    {
        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(Guid id)
    {
        return RedirectToAction(nameof(Mine));
    }
}
