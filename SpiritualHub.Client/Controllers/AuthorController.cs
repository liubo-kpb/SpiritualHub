namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using SpiritualHub.Client.ViewModels.Author;
using SpiritualHub.Services.Interfaces;

public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        IEnumerable<AllAuthorsQueryModel> authors = await _authorService.GetAllAuthorsAsync();
        return View(authors);
    }
}
