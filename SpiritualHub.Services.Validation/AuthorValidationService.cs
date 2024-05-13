namespace SpiritualHub.Services.Validation;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Interfaces;
using Services.Interfaces;
using Client.ViewModels.BaseModels;
using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;

public class AuthorValidationService : ValidationService, IAuthorValidationService
{
    public AuthorValidationService(
        IAuthorService authorService,
        IPublisherService publisherService)
        : base(authorService, publisherService)
    {
    }

    public async Task<IActionResult?> CheckSubscribeActionAsync(string authorId)
    {
        var validationResult = await HandleExistsCheckAsync(authorId);
        
        if (IsUserAdminFunc() && validationResult == null)
        {
            return null;
        }

        return validationResult ?? await CheckUserIsPublisherAsync(authorId);
    }

    public async Task<IActionResult?> HandleFollowActionAsync(string authorId)
    {
        return await HandleExistsCheckAsync(authorId) ?? await CheckFollowingAsync(authorId);
    }

    public async Task<IActionResult?> HandleSubscribeActionAsync(ISubscriptionService subscriptionService, string subscriptionId, string authorId)
    {
        return await CheckSubscribeActionAsync(authorId) ?? await SubscriptionExistsAsync(subscriptionService, subscriptionId, authorId);
    }

    public async Task<IActionResult?> CheckConnectActionAsync(string authorId)
    {
        var validationResult = await HandleExistsCheckAsync(authorId);

        if (IsUserAdminFunc() && validationResult == null)
        {
            return null;
        }

        return validationResult ?? await CheckUserIsPublisherAsync() ?? await this.CheckPublisherConnectionToAuthorAsync(authorId, true);
    }

    public override bool PublisherHasConnectedAuthors(BaseFormModel formModel)
    {
        return true;
    }

    public override async Task<IActionResult?> CheckPublisherConnectionToAuthorAsync(string id, bool isAuthorId)
    {
        if (await _publisherService.IsConnectedToAuthorByUserId(GetUserIdFunc()!, id))
        {
            SetTempDataMessageAction(NotificationType.ErrorMessage, AlreadyAConnectedPublisherErrorMessage);

            return RedirectToAction("MyPublishings");
        }

        return null;
    }

    public override async Task<IActionResult?> CheckModifyPermissionsAsync(string id, bool isAuthorId = false)
    {
        if (IsUserAdminFunc())
        {
            return null!;
        }
        isAuthorId = true;

        return await CheckUserIsPublisherAsync() ?? await base.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);
    }

    private async Task<IActionResult?> CheckUserIsPublisherAsync(string id)
    {
        if (await _publisherService.ExistsByUserIdAsync(GetUserIdFunc()!))
        {
            SetTempDataMessageAction(NotificationType.ErrorMessage, PublishersCannotSubscribeErrorMessage);

            return RedirectToAction("Details", ControllerName, new { id });
        }

        return null!; ;
    }

    private async Task<IActionResult?> SubscriptionExistsAsync(ISubscriptionService subscriptionService, string id, string authorId)
    {
        if (!await subscriptionService.ExistsByIdAsync(id))
        {
            SetTempDataMessageAction(NotificationType.ErrorMessage, SelectValidSubscriptionPlan);

            return RedirectToAction("Subscribe", ControllerName, _authorService.GetAuthorSubscribtionsAsync(authorId));
        }

        return null;
    }

    private async Task<IActionResult?> CheckFollowingAsync(string id)
    {
        if (await _authorService.IsFollowedByUserWithId(id, GetUserIdFunc()!))
        {
            SetTempDataMessageAction(NotificationType.ErrorMessage, AlreadyFollowingAuthorErrorMessage);

            return RedirectToAction("Mine");
        }

        return null;
    }
}
