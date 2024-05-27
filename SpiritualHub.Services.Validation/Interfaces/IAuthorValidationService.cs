namespace SpiritualHub.Services.Validation.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;

public interface IAuthorValidationService : IValidationService
{
    Task<IActionResult?> HandleSubscribeActionAsync(ISubscriptionService subscriptionService, string subscriptionId, string authorId);

    Task<IActionResult?> CheckSubscribeActionAsync(string authorId);

    Task<IActionResult?> CheckConnectActionAsync(string authorId);

    Task<IActionResult?> HandleFollowActionAsync(string authorId);
}
