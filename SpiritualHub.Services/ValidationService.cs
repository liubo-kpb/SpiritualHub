namespace SpiritualHub.Services;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;

public class ValidationService : ControllerBase, IValidationService
{
    protected readonly IAuthorService _authorService;
    protected readonly ICategoryService _categoryService;
    protected readonly IPublisherService _publisherService;

    public ValidationService(
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
    {
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    public string AuthorId { get; set; } = null!;

    public async Task<IActionResult?> IsValidAction(bool entityExists, bool isUserAdmin, string userId, string entityName, ITempDataDictionary tempData)
    {
        if (!entityExists)
        {
            tempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction("All");
        }

        if (isUserAdmin)
        {
            return null!;
        }

        return await CanUseModifyActionAsync(userId, tempData);
    }

    public async Task<IActionResult?> CanUseModifyActionAsync(string userId, ITempDataDictionary tempData)
    {
        var result = await IsUserPublisherAsync(userId, tempData);

        return result ?? await IsUserConnectedPublisherAsync(userId, tempData);
    }

    private async Task<IActionResult?> IsUserPublisherAsync(string userId, ITempDataDictionary tempData)
    {
        if (!await _publisherService.ExistsByUserIdAsync(userId))
        {
            tempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction("Become", nameof(Publisher));
        }

        return null!;
    }

    private async Task<IActionResult?> IsUserConnectedPublisherAsync(string userId, ITempDataDictionary tempData)
    {
        if (!await _publisherService.IsConnectedToAuthorByUserId(userId, AuthorId))
        {
            tempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction("Details", nameof(Author), new { id = AuthorId});
        }

        return null!;
    }
}
