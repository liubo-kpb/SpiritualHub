namespace SpiritualHub.Client.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpiritualHub.Services.Interfaces;
using ViewModels.Home;

public class HomeController : Controller
{
    private readonly IAuthorService _authorService;

    public HomeController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var authorsModel = await _authorService.LastThreeAuthors();

        return View(authorsModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
