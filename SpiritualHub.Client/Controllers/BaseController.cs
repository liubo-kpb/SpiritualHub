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
using ViewModels.Publisher;
using Filters;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
//[ServiceFilter(typeof(CustomValidationFilterAttribute))]
public abstract class BaseController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum> : Controller
    where TViewModel : class
    where TDetailsModel : class
    where TFormModel : BaseFormModel, new()
    where TQueryModel : BaseQueryModel<TViewModel, TSortingEnum>
    where TSortingEnum : Enum
{
    private readonly IValidationService _validationService;

    protected readonly string _entityName;

    protected readonly IAuthorService _authorService;
    protected readonly ICategoryService _categoryService;
    protected readonly IPublisherService _publisherService;

    public BaseController(
        IServiceProvider serviceProvider,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IValidationService validationService,
        string entityName)
    {
        _authorService = serviceProvider.GetRequiredService<IAuthorService>();
        _categoryService = serviceProvider.GetRequiredService<ICategoryService>();
        _publisherService = serviceProvider.GetRequiredService<IPublisherService>();

        _entityName = entityName;

        _validationService = validationService;
        SetValidationServiceProperties(urlHelperFactory, actionContextAccessor);
    }

    protected abstract Task<bool> ExistsAsync(string id);

    protected abstract Task<TQueryModel> GetAllAsync(TQueryModel queryModel, string userId);

    protected abstract Task<TDetailsModel> GetEntityDetails(string id, string userId);

    protected abstract Task<IEnumerable<TViewModel>> GetAllEntitiesByUserId(string userId);

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
            queryModel = await GetAllAsync(queryModel, this.User.GetId()!);

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
            string errorMessage = (await CustomValidateAsync(id))!;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData[ErrorMessage] = errorMessage;

                return RedirectToAction(nameof(All));
            }

            var viewModel = await GetEntityDetails(id, this.User.GetId()!);

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
            var viewModel = await GetAllEntitiesByUserId(this.User.GetId()!);

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
        string userId = this.User.GetId()!;
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
        bool isPublisher = this.User.IsAdmin() || await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var formModel = CreateFormModelInstance();

            await GetFormDetailsAsync(formModel);
            if (!_validationService.PublisherHasConnectedAuthors(formModel)) ;
            {
                TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

                return RedirectToAction(nameof(AuthorController.All), nameof(Author));
            }

            // Need to test
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
        string userId = this.User.GetId()!;
        await ValidateModelAsync(newEntityForm);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }

        var validationResult = await _validationService.CheckModifyPermissionsAsync(newEntityForm.AuthorId!, true);
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

        string userId = this.User.GetId()!;
        var validationResult = await ValidateModifyActionAsync(updatedEntityFrom.Id!, updatedEntityFrom.AuthorId!);
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

    protected virtual async Task GetFormDetailsAsync(TFormModel formModel)
    {
        if (this.User.IsAdmin())
        {
            formModel.Authors = await _authorService.GetAllAsync();

            if (formModel.AuthorId == null)
            {
                formModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                formModel.Publishers = await _authorService.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(formModel.AuthorId);
            }
        }
        else
        {
            formModel.Authors = await _publisherService.GetConnectedAuthorsByUserIdAsync(this.User.GetId()!);
        }
        formModel.Categories = await _categoryService.GetAllAsync();
    }

    protected virtual async Task<IActionResult?> ValidateModifyActionAsync(string entityId, string authorId = null!)
    {
        return await _validationService.CheckModifyActionAsync(entityId, authorId);
    }

    protected virtual async Task ValidateModelAsync(TFormModel formModel)
    {
        if (this.User.IsAdmin())
        {
            if (!(await _publisherService.ExistsByIdAsync(formModel.PublisherId!)))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
            }
            else if (!(await _publisherService.IsConnectedToAuthorByPublisherId(formModel.PublisherId!, formModel.AuthorId!)))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), WrongPublisherErrorMessage);
            }
        }

        bool isExistingAuthor = await _authorService.ExistsAsync(formModel.AuthorId!);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(formModel.AuthorId), string.Format(NoEntityFoundErrorMessage, "author"));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(formModel.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(formModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }
    }

    /// <summary>
    /// Do custom validation for Getting entity details. Returns null by default.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>An empty string if validation is successfull. String with error message if not.</returns>
    protected virtual async Task<string?> CustomValidateAsync(string id)
    {
        return string.Empty;
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

    private string? GetUserId() => this.User.GetId();

    private bool IsUserAdmin() => this.User.IsAdmin();
}
