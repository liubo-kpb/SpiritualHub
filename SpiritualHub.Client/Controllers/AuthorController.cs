namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Author;
using Services.Interfaces;

[Authorize]
public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All()
    {
        IEnumerable<AuthorViewModel> authors = await _authorService.GetAllAuthorsAsync();

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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AuthorFormModel newAuthor)
    {
        return RedirectToAction(nameof(Details), new { id = 1 });
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
