﻿namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using ViewModels.BaseModels;
using Infrastructure.Extensions;
using Services.Validation.Interfaces;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public abstract class ProductController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum>
    : BaseController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum>
    where TViewModel : class
    where TDetailsModel : BaseDetailsViewModel
    where TFormModel : BaseFormModel, new()
    where TQueryModel : BaseQueryModel<TViewModel, TSortingEnum>
    where TSortingEnum : Enum
{
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

        string userId = this.User.GetId()!;
        if (!this.User.IsAdmin())
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
            await RemoveAsync(id, this.User.GetId()!);
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

    protected virtual string GetAction()
    {
        return $"get {_entityName}";
    }

    protected virtual string RemoveAction()
    {
        return $"remove {_entityName} from your space";
    }
}
