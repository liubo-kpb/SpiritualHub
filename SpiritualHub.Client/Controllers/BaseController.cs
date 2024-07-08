namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Data.Models;
using Services.Interfaces;
using Services.Validation.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Enums;
using ViewModels.BaseModels;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
public abstract class BaseController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum> : Controller
    where TViewModel : class
    where TDetailsModel : class
    where TFormModel : BaseFormModel, new()
    where TQueryModel : BaseQueryModel<TViewModel, TSortingEnum>
    where TSortingEnum : Enum
{

    protected readonly string _entityName;

    protected readonly ICategoryService _categoryService;
    protected readonly IPublisherService _publisherService;
    protected readonly IValidationService _validationService;

    public BaseController(
        IServiceProvider serviceProvider,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IValidationService validationService,
        string entityName)
    {
        _categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        _publisherService = serviceProvider.GetRequiredService<IPublisherService>();

        _entityName = entityName;

        _validationService = validationService;
        SetValidationServiceProperties(urlHelperFactory, actionContextAccessor);
    }

    protected abstract Task<bool> ExistsAsync(string id);

    protected abstract Task<TQueryModel> GetAllAsync(TQueryModel queryModel, string userId);

    protected abstract Task<TDetailsModel> GetEntityDetails(string id, string userId);

    protected abstract Task<IEnumerable<TViewModel>> GetAllEntitiesByUserIdAsync(string userId);

    protected abstract Task<IEnumerable<TViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId);

    protected abstract Task<string> CreateAsync(TFormModel newEntity);

    protected abstract Task<TFormModel> GetEntityInfoAsync(string id);

    protected abstract Task EditAsync(TFormModel updatedEntityFrom);

    protected abstract Task<string> GetAuthorIdAsync(string entityId);

    [AllowAnonymous]
    [HttpGet]
    public virtual async Task<IActionResult> All([FromQuery] TQueryModel queryModel)
    {
        try
        {
            queryModel = await GetAllAsync(queryModel, GetUserId()!);

            var categories = await _categoryService.GetAllAsync();
            queryModel.Categories = categories.Select(c => c.Name);

            return View(queryModel);
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}s");
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [AllowAnonymous]
    [HttpGet]
    public virtual async Task<IActionResult> Details(string id)
    {
        if (!await ExistsAsync(id))
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);
            return RedirectToAction(nameof(All));
        }

        try
        {
            string errorMessage = await ValidateAccessibilityAsync(id);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData[ErrorMessage] = errorMessage;

                return RedirectToAction(nameof(All));
            }

            var viewModel = await GetEntityDetails(id, GetUserId()!);

            return View(viewModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"loading {_entityName}");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Mine()
    {
        try
        {
            var viewModel = await GetAllEntitiesByUserIdAsync(GetUserId()!);

            return View(viewModel);
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {_entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> MyPublishings()
    {
        string userId = GetUserId()!;
        if (!await _publisherService.ExistsByUserIdAsync(userId))
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;
            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var publisherId = await _publisherService.GetPublisherIdAsync(userId);
            var viewModel = await GetEntitiesByPublisherIdAsync(publisherId!, userId);

            return View(viewModel);
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {_entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Add()
    {
        bool isPublisher = IsUserAdmin() || await _publisherService.ExistsByUserIdAsync(GetUserId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var formModel = CreateFormModelInstance();
            await GetFormDetailsAsync(formModel);

            return View(nameof(Add), formModel);
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, "load page");
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add(TFormModel newEntityForm)
    {
        string userId = GetUserId()!;
        await ValidateModelAsync(newEntityForm);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }

        var validationResult = await ValidateAddActionAsync(newEntityForm);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            newEntityForm.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            string id = await CreateAsync(newEntityForm);
            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create {_entityName}");

            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Edit(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var entityFormModel = await GetEntityInfoAsync(id);
            await GetFormDetailsAsync(entityFormModel!);

            return View(entityFormModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Edit(TFormModel updatedEntityFrom)
    {
        await ValidateModelAsync(updatedEntityFrom);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(updatedEntityFrom);

            return View(updatedEntityFrom);
        }

        string userId = GetUserId()!;
        var validationResult = await ValidateModifyActionAsync(updatedEntityFrom.Id!, await GetAuthorIdAsync(updatedEntityFrom.Id!));
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            updatedEntityFrom.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            await EditAsync(updatedEntityFrom);
            TempData[SuccessMessage] = string.Format(EditSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id = updatedEntityFrom.Id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"edit {_entityName}");

            await GetFormDetailsAsync(updatedEntityFrom);

            return View(updatedEntityFrom);
        }
    }

    protected virtual TFormModel CreateFormModelInstance()
    {
        return new TFormModel();
    }

    protected virtual async Task GetFormDetailsAsync(TFormModel formModel, bool getAllPublishers = true)
    {
        if (getAllPublishers && IsUserAdmin())
        {
            formModel.Publishers = await _publisherService.GetAllAsync();
        }

        formModel.Categories = await _categoryService.GetAllAsync();
    }

    protected virtual async Task<IActionResult?> ValidateModifyActionAsync(string entityId, string authorId = null!)
    {
        return await _validationService.CheckModifyActionAsync(entityId, authorId);
    }

    protected virtual async Task<IActionResult?> ValidateAddActionAsync(TFormModel newEntityForm)
    {
        return await _validationService.CheckUserIsPublisherAsync();
    }

    protected virtual async Task<bool> ValidateModifyPermissionsAsync(string id, bool isAuthorId = false)
    {
        return await _validationService.CanModifyAsync(id, isAuthorId);
    }

    protected virtual async Task ValidateModelAsync(TFormModel formModel)
    {
        if (IsUserAdmin())
        {
            if (!(await _publisherService.ExistsByIdAsync(formModel.PublisherId!)))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
            }
            else if (!(await _publisherService.IsConnectedToAuthorByPublisherId(formModel.PublisherId!, await GetAuthorIdAsync(formModel.Id!))))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), WrongPublisherErrorMessage);
            }
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(formModel.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(formModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }
    }

    /// <summary>
    /// Do custom validation for Getting entity details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An empty string if validation is successfull. String with error message if not.</returns>
    protected virtual async Task<string> ValidateAccessibilityAsync(string id)
    {
        return await Task.FromResult(string.Empty);
    }

    protected IActionResult ReturnToHome()
    {
        if (string.IsNullOrEmpty(TempData[ErrorMessage]?.ToString() ?? string.Empty))
        {
            TempData[ErrorMessage] = InvalidRequestErrorMessage;
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    private void SetValidationServiceProperties(IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor)
    {
        var controllerName = this.GetType().Name;
        _validationService.ControllerName = controllerName[..controllerName.IndexOf("Controller")];
        _validationService.EntityName = this._entityName;
        _validationService.UrlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext!);

        _validationService.GetAuthorIdAsyncFunc = this.GetAuthorIdAsync;
        _validationService.ExistsAsyncFunc = this.ExistsAsync;
        _validationService.GetUserIdFunc = this.GetUserId;
        _validationService.IsUserAdminFunc = this.IsUserAdmin;
        _validationService.SetTempDataMessageAction = this.SetTempDataMessage;
    }

    private void SetTempDataMessage(NotificationType notificationType, string message)
    {
        string notificationString = notificationType switch
        {
            NotificationType.ErrorMessage => ErrorMessage,
            NotificationType.WarningMessage => WarningMessage,
            NotificationType.InformationMessage => InformationMessage,
            NotificationType.SuccessMessage => SuccessMessage,
            _ => InformationMessage
        };

        TempData[notificationString] = message;
    }

    protected virtual string? GetUserId() => this.User.GetId();

    protected virtual bool IsUserAdmin() => this.User.IsAdmin();
}
