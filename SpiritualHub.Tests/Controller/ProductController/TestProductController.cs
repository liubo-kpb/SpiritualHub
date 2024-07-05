namespace SpiritualHub.Tests.Controller.ProductController;

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Client.Controllers;
using Client.ViewModels.BaseModels;
using System.Collections.Generic;

internal class TestProductController
    : ProductController<BaseDetailsViewModel, BaseDetailsViewModel, ProductFormModel, BaseQueryModel<BaseDetailsViewModel, Enum>, Enum>
{
    private const string NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE = "Methods are being tested in namespace SpiritualHub.Tests.Controller.BaseController;";

    public TestProductController(IServiceProvider serviceProvider, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, string entityName)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, entityName)
    {
    }

    public int GetAsyncCounter { get; set; }

    public int RemoveAsyncCounter { get; set; }

    public int DeleteAsyncCounter { get; set; }

    public int ShowAsyncCounter { get; set; }

    public int HideAsyncCounter { get; set; }

    public int HasEntityAsyncCounter { get; set; }

    public int AlreadyHasEntityErrorMessageCounter { get; set; }

    public int GetEntitySuccessMessageCounter { get; set; }

    public int RemoveEntitySuccessMessageCounter { get; set; }

    protected override async Task GetAsync(string id, string userId)
    {
        GetAsyncCounter++;
        await Task.CompletedTask;
    }

    protected override async Task RemoveAsync(string id, string userId)
    {
        RemoveAsyncCounter++;
        await Task.CompletedTask;

    }

    protected override async Task DeleteAsync(string id)
    {
        await Task.CompletedTask;
        DeleteAsyncCounter++;
    }

    protected override async Task ShowAsync(string id)
    {
        await Task.CompletedTask;
        ShowAsyncCounter++;
    }

    protected override async Task HideAsync(string id)
    {
        await Task.CompletedTask;
        HideAsyncCounter++;
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        HasEntityAsyncCounter++;

        return await Task.FromResult(true);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        AlreadyHasEntityErrorMessageCounter++;

        return "AlreadyHasEntityErrorMessage";
    }

    protected override string GetEntitySuccessMessage()
    {
        GetEntitySuccessMessageCounter++;

        return "Got Entity";
    }

    protected override string RemoveEntitySuccessMessage()
    {
        RemoveEntitySuccessMessageCounter++;

        return "Removed Entity";
    }

    // Irrelevant code. BaseController methods are tested in TestBaseController.cs
    protected override Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException(NOT_IMPLEMENTED_EXCEPTION_ERROR_MESSAGE);
    }

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

    protected override Task<ProductFormModel> GetEntityInfoAsync(string id)
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
}
