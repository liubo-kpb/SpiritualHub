namespace SpiritualHub.Services.Validation;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Interfaces;
using Services.Interfaces;
using Data.Models;
using Client.Infrastructure.Extensions;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;

public class ValidationService : IValidationService
{
    private readonly IUrlHelper _urlHelper;

    protected readonly IAuthorService _authorService;
    protected readonly IPublisherService _publisherService;

    public ValidationService(
        IUrlHelper urlHelper,
        IAuthorService authorService,
        IPublisherService publisherService)
    {
        _urlHelper = urlHelper;
        _authorService = authorService;
        _publisherService = publisherService;
    }

    public string ControllerName { get; set; } = null!;

    public ClaimsPrincipal User { get; set; } = null!;

    public ITempDataDictionary TempData { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public Func<string, Task<bool>> ExistsFunc { get; set; } = null!;

    public Func<string, Task<string>> GetAuthorIdFunc { get; set; } = null!;

    public virtual async Task<IActionResult?> CheckModifyActionAsync(string entityId)
    {
        var validationResult = await HandleExistsCheckAsync(entityId);

        if (this.User.IsAdmin() && validationResult == null)
        {
            return null!;
        }

        return validationResult ?? await CheckModifyPermissionsAsync(entityId);
    }

    public virtual async Task<IActionResult> HandleExistsCheckAsync(string entityId)
    {
        if (!await ExistsFunc(entityId))
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, EntityName);

            return RedirectToAction("All");
        }

        return null!;
    }

    public virtual async Task<IActionResult?> CheckModifyPermissionsAsync(string entityId = null!)
    {
        return await CheckUserIsPublisherAsync() ?? await CheckPublisherIsConnectionToAuthorAsync(entityId);
    }

    public virtual async Task<IActionResult?> CheckUserIsPublisherAsync()
    {
        if (!await _publisherService.ExistsByUserIdAsync(this.User.GetId()!))
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction("Become", nameof(Publisher));
        }

        return null!;
    }

    public virtual async Task<IActionResult?> CheckPublisherIsConnectionToAuthorAsync(string? entityId)
    {
        this.AuthorId ??= await GetAuthorIdFunc(entityId!);
        if (this.AuthorId == null)
        {
            throw new ArgumentException();
        }

        if (!await _publisherService.IsConnectedToAuthorByUserId(this.User.GetId()!, this.AuthorId))
        {
            TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction("Details", nameof(Author), new { id = this.AuthorId });
        }
        
        return null!;
    }

    protected IActionResult RedirectToAction(string action, string? controller = null, object? routeValue = null)
    {
        controller ??= ControllerName;
        string actionUrl = _urlHelper.Action(action, controller)!;
        if (routeValue != null)
        {
            actionUrl = _urlHelper.Action(action, controller, routeValue)!;
        }

        return new RedirectResult(actionUrl);
    }
}
