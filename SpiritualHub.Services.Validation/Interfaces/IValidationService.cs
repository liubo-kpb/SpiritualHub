namespace SpiritualHub.Services.Validation.Interfaces;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Client.ViewModels.BaseModels;
using SpiritualHub.Client.Infrastructure.Enums;

/// <summary>
/// Validation checks when an Action is submitted. Methods return <see cref="IActionResult"/> depending on the result. Intended to return <c>null</c> if action passes validation.
/// </summary>
public interface IValidationService
{
    string ControllerName { get; set; }

    public IUrlHelper UrlHelper { get; set; }

    string EntityName { get; set; }

    Func<string, Task<bool>> ExistsAsyncFunc { get; set; }

    Func<string, Task<string>> GetAuthorIdAsyncFunc {get; set;}

    Func<string?> GetUserIdFunc {get; set;}

    Func<bool> IsUserAdminFunc { get; set; }

    Action<NotificationType, string> SetTempDataMessageAction { get; set; }

    /// <summary>
    /// Validates entity existence in the DB and user modify permissions with <see cref="CheckModifyPermissionsAsync(string, ITempDataDictionary)"/>.
    /// </summary>
    /// <param name="entityExists"></param>
    /// <param name="isUserAdmin"></param>
    /// <param name="userId"></param>
    /// <param name="entityName"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> CheckModifyActionAsync(string entityId, string? authorId);

    Task<IActionResult?> HandleExistsCheckAsync(string entityId);

    /// <summary>
    /// Validates user modify permissions for respective entity and action.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> CheckModifyPermissionsAsync(string id, bool isAuthorId = false);

    Task<IActionResult?> CheckUserIsPublisherAsync();

    Task<IActionResult?> CheckPublisherConnectionToAuthorAsync(string id, bool isAuthorId);

    bool                 PublisherHasConnectedAuthors(BaseFormModel formModel);
}
