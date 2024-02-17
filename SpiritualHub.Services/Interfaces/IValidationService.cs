namespace SpiritualHub.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public interface IValidationService
{
    string AuthorId { get; set; }

    Task<IActionResult?> IsValidAction(bool entityExists, bool isUserAdmin, string userId, string entityName, ITempDataDictionary tempData);

    Task<IActionResult?> CanUseModifyActionAsync(string userId, ITempDataDictionary tempData);
}
