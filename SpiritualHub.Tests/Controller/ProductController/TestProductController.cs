namespace SpiritualHub.Tests.Controller.ProductController;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Client.Controllers;
using Client.ViewModels.BaseModels;

using static Extensions.Common.TestMessageConstants;

internal class TestProductController
    : ProductController<BaseDetailsViewModel, BaseDetailsViewModel, ProductFormModel, BaseQueryModel<BaseDetailsViewModel, Enum>, Enum>
{
    private const string NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE = "Methods are being tested in namespace SpiritualHub.Tests.Controller.BaseController.";

    public TestProductController(IServiceProvider serviceProvider, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, string entityName)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, entityName)
    {
        this.HasEntityAsyncFlag = false;
        this.ExistsFlag = true;
        this.IsAdmin = false;
        this.ThrowExceptionFlag = false;
        this.ThrowNotImplementedExceptionFlag = false;
    }

    public string UserId => "userId";

    public string AlreadyHasEntityMessage => "AlreadyHasEntityErrorMessage";

    public string GotEntityMessage => $"Got {_entityName}";

    public string RemovedEntityMessage => $"Removed {_entityName}";

    #region Flags and Counters
    public bool IsAdmin { get; set; }

    public bool ThrowExceptionFlag { get; set; }

    public int ThrowExceptionCounter { get; set; }

    public bool ThrowNotImplementedExceptionFlag { get; set; }

    public int ThrowNotImplementedExceptionCounter { get; set; }

    public int ValidateAccessibilityAsyncCounter { get; set; }
    #endregion

    #region Abstract methods flags and counters
    public bool ExistsFlag { get; set; }

    public int ExistsCounter { get; set; }

    public int GetAsyncCounter { get; set; }

    public int RemoveAsyncCounter { get; set; }

    public int GetEntityInfoCounter { get; set; }

    public int DeleteAsyncCounter { get; set; }

    public int ShowAsyncCounter { get; set; }

    public int HideAsyncCounter { get; set; }

    public bool HasEntityAsyncFlag { get; set; }

    public int HasEntityAsyncCounter { get; set; }

    public int AlreadyHasEntityCounter { get; set; }

    public int GetEntitySuccessMessageCounter { get; set; }

    public int RemoveEntitySuccessMessageCounter { get; set; }
    #endregion

    #region Abstract methods from ProductController
    protected override async Task GetAsync(string id, string userId)
    {
        GetAsyncCounter++;
        ThrowException();
        await Task.CompletedTask;
    }

    protected override async Task RemoveAsync(string id, string userId)
    {
        RemoveAsyncCounter++;
        ThrowException();
        await Task.CompletedTask;
    }

    protected override async Task DeleteAsync(string id)
    {
        DeleteAsyncCounter++;
        ThrowException();
        await Task.CompletedTask;
    }

    protected override async Task ShowAsync(string id)
    {
        ShowAsyncCounter++;
        ThrowException();
        await Task.CompletedTask;
    }

    protected override async Task HideAsync(string id)
    {
        HideAsyncCounter++;
        ThrowException();
        await Task.CompletedTask;
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        HasEntityAsyncCounter++;
        return await Task.FromResult(HasEntityAsyncFlag);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        AlreadyHasEntityCounter++;
        return AlreadyHasEntityMessage;
    }

    protected override string GetEntitySuccessMessage()
    {
        GetEntitySuccessMessageCounter++;
        return GotEntityMessage;
    }

    protected override string RemoveEntitySuccessMessage()
    {
        RemoveEntitySuccessMessageCounter++;
        return RemovedEntityMessage;
    }
    #endregion

    #region BaseController.cs methods
    protected override Task<bool> ExistsAsync(string id)
    {
        ExistsCounter++;

        return Task.FromResult(ExistsFlag);
    }

    protected override Task<ProductFormModel> GetEntityInfoAsync(string id)
    {
        GetEntityInfoCounter++;
        ThrowException();
        return Task.FromResult(new ProductFormModel());
    }

    /// <summary>
    /// Irrelevant code. BaseController methods are tested in <see cref="BaseController.TestBaseController"/>.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected override Task<BaseQueryModel<BaseDetailsViewModel, Enum>> GetAllAsync(BaseQueryModel<BaseDetailsViewModel, Enum> queryModel, string userId)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task<BaseDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task<IEnumerable<BaseDetailsViewModel>> GetAllEntitiesByUserIdAsync(string userId)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task<IEnumerable<BaseDetailsViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task<string> CreateAsync(ProductFormModel newEntity)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task EditAsync(ProductFormModel updatedEntityFrom)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

    protected override Task<string> GetAuthorIdAsync(string entityId)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }
    #endregion

    private void ThrowException()
    {
        if (ThrowExceptionFlag)
        {
            ThrowExceptionCounter++;

            throw new Exception();
        }
        else if (ThrowNotImplementedExceptionFlag)
        {
            ThrowNotImplementedExceptionCounter++;

            throw new NotImplementedException(TestErrorMessageForExceptions);
        }
    }

    protected override string? GetUserId() => UserId;

    protected override bool IsUserAdmin() => IsAdmin;
}
