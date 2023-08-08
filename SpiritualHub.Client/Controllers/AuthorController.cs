namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Author;
using Services.Interfaces;
using SpiritualHub.Client.Infrastructure.Extensions;

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
    public async Task<IActionResult> All()
    {
        IEnumerable<AuthorViewModel> authors = await _authorService.GetAllAsync();

        AllAuthorsQueryModel queryModel = new AllAuthorsQueryModel()
        {
            Authors = authors
        };

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        return View(new AllAuthorsQueryModel());
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        return View(new AuthorDetailsViewModel());
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

        var isExistingCategory = await _categoryService.ExistsAsync(newAuthor.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(newAuthor.CategoryId), "Category doesn't exist. Please select a valid category");
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
    public async Task<IActionResult> Edit()
    {
        return View(new AuthorFormModel());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, AuthorFormModel author)
    {
        return RedirectToAction(nameof(Details), new { id = 1 });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        return View(new AuthorFormModel());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(AuthorDetailsViewModel author)
    {
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
