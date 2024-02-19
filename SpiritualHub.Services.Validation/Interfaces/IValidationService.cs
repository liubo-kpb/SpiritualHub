namespace SpiritualHub.Services.Validation.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;

/// <summary>
/// Validation checks when an Action is submitted. Methods return <see cref="IActionResult"/> depending on the result. Intended to return <c>null</c> if action passes validation.
/// </summary>
public interface IValidationService
{
    string ControllerName { get; set; }

    ClaimsPrincipal User { get; set; }

    ITempDataDictionary TempData { get; set; }

    string EntityName { get; set; }

    string AuthorId { get; set; }

    Func<string, Task<bool>> ExistsFunc { get; set; }

    Func<string, Task<string>> GetAuthorIdFunc {get; set;}

    /// <summary>
    /// Validates entity existence in the DB and user modify permissions with <see cref="CheckModifyPermissionsAsync(string, ITempDataDictionary)"/>.
    /// </summary>
    /// <param name="entityExists"></param>
    /// <param name="isUserAdmin"></param>
    /// <param name="userId"></param>
    /// <param name="entityName"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> CheckModifyActionAsync(string entityId);

    Task<IActionResult> HandleExistsCheckAsync(string entityId);

    /// <summary>
    /// Validates user modify permissions for respective action.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> CheckModifyPermissionsAsync(string entityId = null!);

    Task<IActionResult?> CheckUserIsPublisherAsync();

    Task<IActionResult?> CheckPublisherIsConnectionToAuthorAsync(string? entityId);
}
