namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using ViewModels.BaseModels;
using ViewModels.Publisher;
using Services.Interfaces;
using Services.Validation.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using SpiritualHub.Services.Validation;

public abstract class ProductController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum>
    : BaseController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum>
    where TViewModel : class
    where TDetailsModel : BaseDetailsViewModel
    where TFormModel : ProductFormModel, new()
    where TQueryModel : BaseQueryModel<TViewModel, TSortingEnum>
    where TSortingEnum : Enum
{
    protected readonly IAuthorService _authorService;

    protected ProductController(
        IServiceProvider serviceProvider,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        string entityName)
        : base(serviceProvider,
            urlHelperFactory,
            actionContextAccessor,
            serviceProvider.GetRequiredService<IValidationService>(),
            entityName)
    {
        _authorService = serviceProvider.GetRequiredService<IAuthorService>();
    }

    protected abstract Task GetAsync(string id, string userId);

    protected abstract Task RemoveAsync(string id, string userId);

    protected abstract Task DeleteAsync(string id);

    protected abstract Task ShowAsync(string id);

    protected abstract Task HideAsync(string id);

    protected abstract Task<bool> HasEntityAsync(string id, string usedId);

    protected abstract string AlreadyHasEntityErrorMessage();

    protected abstract string GetEntitySuccessMessage();

    protected abstract string RemoveEntitySuccessMessage();

    [HttpPost]
    public virtual async Task<IActionResult> Get(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);
            return RedirectToAction(nameof(All));
        }

        string userId = GetUserId()!;
        if (!IsUserAdmin())
        {
            bool hasEntity = await HasEntityAsync(id, userId);
            if (hasEntity)
            {
                TempData[ErrorMessage] = AlreadyHasEntityErrorMessage();

                return RedirectToAction(nameof(Details), new { id });
            }
        }

        try
        {
            await GetAsync(id, userId);
            TempData[SuccessMessage] = GetEntitySuccessMessage();

            return RedirectToAction(nameof(Mine));
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return ReturnToHome();
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, GetAction());

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Remove(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await RemoveAsync(id, GetUserId()!);
            TempData[SuccessMessage] = RemoveEntitySuccessMessage();

            return RedirectToAction(nameof(Mine));
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return ReturnToHome();
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, RemoveAction());

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Show(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await ShowAsync(id);
            TempData[SuccessMessage] = string.Format(ShowEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return ReturnToHome();
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"show the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Hide(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await HideAsync(id);
            TempData[SuccessMessage] = string.Format(HideEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;

            return ReturnToHome();
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"hide the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Delete(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var viewModel = await GetEntityInfoAsync(id);

            return View(viewModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Delete(TDetailsModel detailsViewModel)
    {
        var validationResult = await ValidateModifyActionAsync(detailsViewModel.Id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await DeleteAsync(detailsViewModel.Id);
            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"delete {_entityName}");

            return View(new { id = detailsViewModel.Id });
        }
    }

    public override async Task<IActionResult> Add()
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
            if (!formModel.Authors.Any())
            {
                TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

                return RedirectToAction(nameof(AuthorController.All), nameof(Author));
            }

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

    protected override async Task GetFormDetailsAsync(TFormModel formModel, bool getAllPublishers = false)
    {
        if (IsUserAdmin())
        {
            formModel.Authors = await _authorService.GetAllAsync();

            if (formModel.AuthorId == null)
            {
                getAllPublishers = true;
            }
            else
            {
                formModel.Publishers = await _authorService.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(formModel.AuthorId);
            }
        }
        else
        {
            formModel.Authors = await _publisherService.GetConnectedAuthorsByUserIdAsync(GetUserId()!);
        }

        await base.GetFormDetailsAsync(formModel, getAllPublishers);
    }

    protected override async Task ValidateModelAsync(TFormModel formModel)
    {
        await base.ValidateModelAsync(formModel);

        if (IsUserAdmin() && !ModelState.ContainsKey(nameof(formModel.PublisherId)) && !(await _publisherService.IsConnectedToAuthorByPublisherId(formModel.PublisherId!, formModel.AuthorId)))
        {
            ModelState.AddModelError(nameof(formModel.PublisherId), WrongPublisherErrorMessage);
        }

        bool isExistingAuthor = await _authorService.ExistsAsync(formModel.AuthorId);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(formModel.AuthorId), string.Format(NoEntityFoundErrorMessage, "author"));
        }
    }

    protected override async Task<IActionResult?> ValidateAddActionAsync(TFormModel newEntityForm)
    {
        return await _validationService.CheckModifyPermissionsAsync(newEntityForm.AuthorId, true);
    }

    protected virtual string GetAction()
    {
        return $"get {_entityName}";
    }

    protected virtual string RemoveAction()
    {
        return $"remove {_entityName} from your space";
    }
}
