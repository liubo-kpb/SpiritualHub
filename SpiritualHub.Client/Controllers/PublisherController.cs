namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Publisher;
using Infrastructure.Extensions;
using SpiritualHub.Services.Interfaces;

using static Common.NotificationMessagesConstants;
using SpiritualHub.Data.Repository;
using SpiritualHub.Data.Models;

[Authorize]
public class PublisherController : Controller
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    public async Task<IActionResult> Become()
    {
        string userId = User.GetId()!.ToUpper();
        bool isPublisher = await _publisherService.ExistsById(userId);

        if (isPublisher)
        {
            TempData[ErrorMessage] = "You are already a publisher!";

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Become(BecomePublisherFormModel model)
    {
        string userId = User.GetId()!;

        bool isAgent = await _publisherService.ExistsById(userId);
        if (isAgent)
        {
            TempData[ErrorMessage] = "You are already a publisher!";

            return RedirectToAction("Index", "Home");
        }

        bool isPhoneNumberTaken = await _publisherService.UserWithPhoneNumberExists(model.PhoneNumber);
        if (isPhoneNumberTaken)
        {
            ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number is already registered for a publisher!");
        }

        bool hasSubscriptions = await _publisherService.UserHasSubscriptions(userId);
        if (hasSubscriptions)
        {
            TempData[ErrorMessage] = "You are already a publisher!";

            return RedirectToAction("Mine", "Author");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _publisherService.Create(userId, model.PhoneNumber);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "An unexpected error occurred when attempting to make you an agent. Please try again later!";

            return View(model);
        }

        return RedirectToAction(nameof(AuthorController.All), "Author");
    }
}
