namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Client.ViewModels.Publisher;

[Authorize]
public class PublisherController : Controller
{
    [HttpPost]
    public IActionResult Become(BecomePublisherFormModel publisher)
    {
        return RedirectToAction(nameof(AuthorController.All), "Authors");
    }
}
