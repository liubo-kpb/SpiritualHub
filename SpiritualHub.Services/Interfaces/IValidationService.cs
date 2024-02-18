namespace SpiritualHub.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public interface IValidationService
{
    string AuthorId { get; set; }

    /// <summary>
    /// Sets coresponding fields for internal logic of the class. Used if <see cref="AuthorId"/> is <c>null</c> and/or not available to be set.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="getAuthorId"></param>
    void SetFields(string id, Func<string, Task<string>> getAuthorId);

    /// <summary>
    /// Validates entity existence in the DB and user modify permissions with <see cref="ModifyPermissionsAsync(string, ITempDataDictionary)"/>.
    /// </summary>
    /// <param name="entityExists"></param>
    /// <param name="isUserAdmin"></param>
    /// <param name="userId"></param>
    /// <param name="entityName"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> ActionAsync(bool entityExists, bool isUserAdmin, string userId, string entityName, ITempDataDictionary tempData);

    /// <summary>
    /// Validates user modify permissions for respective action.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tempData"></param>
    /// <returns>Returns <see cref="IActionResult"/> based on validation result and logic.</returns>
    Task<IActionResult?> ModifyPermissionsAsync(string userId, ITempDataDictionary tempData);
}
