﻿namespace SpiritualHub.Services.Validation;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Interfaces;
using Services.Interfaces;
using Data.Models;
using Client.Infrastructure.Extensions;
using Client.ViewModels.BaseModels;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;

public class ValidationService : IValidationService
{
    protected readonly IAuthorService _authorService;
    protected readonly IPublisherService _publisherService;


    public ValidationService(
        IAuthorService authorService,
        IPublisherService publisherService)
    {
        _authorService = authorService;
        _publisherService = publisherService;
    }

    public string ControllerName { get; set; } = null!;

    public ClaimsPrincipal User { get; set; } = null!;

    public ITempDataDictionary TempData { get; set; } = null!;

    public IUrlHelper UrlHelper { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public Func<string, Task<bool>> ExistsAsyncFunc { get; set; } = null!;

    public Func<string, Task<string>> GetAuthorIdAsyncFunc { get; set; } = null!;

    public virtual async Task<IActionResult?> CheckModifyActionAsync(string entityId, string? authorId)
    {
        var validationResult = await HandleExistsCheckAsync(entityId);

        if (this.User.IsAdmin() && validationResult == null)
        {
            return null!;
        }

        string id = entityId;
        bool isAuthorId = false;
        if (!string.IsNullOrEmpty(authorId))
        {
            id = authorId;
            isAuthorId = true;
        }

        return validationResult ?? await CheckModifyPermissionsAsync(id, isAuthorId);
    }

    public virtual async Task<IActionResult> HandleExistsCheckAsync(string entityId)
    {
        if (!await ExistsAsyncFunc(entityId))
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, EntityName);

            return RedirectToAction("All", ControllerName);
        }

        return null!;
    }

    public virtual async Task<IActionResult?> CheckModifyPermissionsAsync(string id, bool isAuthorId = false)
    {
        return await CheckUserIsPublisherAsync() ?? await CheckPublisherConnectionToAuthorAsync(id, isAuthorId);
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

    public virtual async Task<IActionResult?> CheckPublisherConnectionToAuthorAsync(string id, bool isAuthorId)
    {
        if (!isAuthorId)
        {
            id = await this.GetAuthorIdAsyncFunc(id);
        }

        if (!await _publisherService.IsConnectedToAuthorByUserId(this.User.GetId()!, id))
        {
            TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

            return RedirectToAction("Details", nameof(Author), new { id });
        }

        return null!;
    }

    protected IActionResult RedirectToAction(string action, string? controller = null, object? routeValue = null)
    {
        controller ??= ControllerName;
        string actionUrl = UrlHelper.Action(action, controller)!;
        if (routeValue != null)
        {
            actionUrl = UrlHelper.Action(action, controller, routeValue)!;
        }

        return new RedirectResult(actionUrl);
    }

    public virtual bool PublisherHasConnectedAuthors(BaseFormModel formModel)
    {
        return formModel.Authors.Any();
    }

    public void SetUrlHelper(IUrlHelper urlHelper)
    {
        UrlHelper = urlHelper;
    }
}
