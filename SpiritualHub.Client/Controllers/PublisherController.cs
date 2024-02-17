namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Publisher;
using Infrastructure.Extensions;
using Services.Interfaces;

using static Common.NotificationMessagesConstants;
using static Common.SuccessMessageConstants;
using static Common.ErrorMessagesConstants;

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
        string userId = User.GetId()!;
        bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);

        if (isPublisher)
        {
            TempData[ErrorMessage] = AlreadyAPublisherErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Become(BecomePublisherFormModel model)
    {
        string userId = User.GetId()!;

        bool isAgent = await _publisherService.ExistsByUserIdAsync(userId);
        if (isAgent)
        {
            TempData[ErrorMessage] = AlreadyAPublisherErrorMessage;

            return RedirectToAction("Index", "Home");
        }

        bool isPhoneNumberTaken = await _publisherService.UserWithPhoneNumberExists(model.PhoneNumber);
        if (isPhoneNumberTaken)
        {
            ModelState.AddModelError(nameof(model.PhoneNumber), PhoneAlreadyRegisteredErrorMessage);
        }

        bool hasSubscriptions = await _publisherService.UserHasSubscriptions(userId);
        if (hasSubscriptions)
        {
            TempData[ErrorMessage] = UserHasSubscriptionErrorMessage;

            return RedirectToAction(nameof(AuthorController.Mine), "Author");
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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, "make you a publisher");

            return View(model);
        }

        TempData[SuccessMessage] = BecomePublisherSuccessfulMessage;
        return RedirectToAction(nameof(AuthorController.All), "Author");
    }
}
