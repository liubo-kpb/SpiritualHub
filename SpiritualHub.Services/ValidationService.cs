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
    private string _entityId = null!;
    private Func<string, Task<string>> _getAuthorId = null!;

    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;

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


    public void SetFields(string id, Func<string, Task<string>> getAuthorId)
    {
        _entityId = id;
        _getAuthorId = getAuthorId;
    }

    public async Task<IActionResult?> ActionAsync(bool entityExists, bool isUserAdmin, string userId, string entityName, ITempDataDictionary tempData)
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

        return await ModifyPermissionsAsync(userId, tempData);
    }

    public async Task<IActionResult?> ModifyPermissionsAsync(string userId, ITempDataDictionary tempData)
    {
        var result = await UserIsPublisherAsync(userId, tempData);

        return result ?? await PublisherIsConnectionToAuthorAsync(userId, tempData);
    }

    private async Task<IActionResult?> UserIsPublisherAsync(string userId, ITempDataDictionary tempData)
    {
        if (!await _publisherService.ExistsByUserIdAsync(userId))
        {
            tempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction("Become", nameof(Publisher));
        }

        return null!;
    }

    private async Task<IActionResult?> PublisherIsConnectionToAuthorAsync(string userId, ITempDataDictionary tempData)
    {
        this.AuthorId ??= await _getAuthorId(_entityId);
        if (!await _publisherService.IsConnectedToAuthorByUserId(userId, this.AuthorId))
        {
            tempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction("Details", nameof(Author), new { id = this.AuthorId });
        }

        return null!;
    }
}
