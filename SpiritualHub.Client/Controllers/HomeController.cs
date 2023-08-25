namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;

using static Common.GeneralApplicationConstants;

public class HomeController : Controller
{
    private readonly IAuthorService _authorService;

    public HomeController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        if (this.User.IsInRole(AdminRoleName))
        {
            return RedirectToAction("Index", "Home", new {area = "Admin"});
        }

        var authorsModel = await _authorService.LastThreeAuthors();

        return View(authorsModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 400)
        {
            return View("Error400");
        }
        else if (statusCode == 401)
        {
            return View("Error401");
        }

        return View("Error");
    }
}
