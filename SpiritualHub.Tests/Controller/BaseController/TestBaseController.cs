﻿namespace SpiritualHub.Tests.Controller.BaseController;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Services.Validation.Interfaces;
using Client.Controllers;
using Client.ViewModels.BaseModels;

using static Extensions.Common.TestErrorMessagesConstants;

internal class TestBaseController
    : BaseController<EmptyViewModel, BaseDetailsViewModel, BaseFormModel, BaseQueryModel<EmptyViewModel, Enum>, Enum>
{
    public TestBaseController(IServiceProvider serviceProvider, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, IValidationService validationService, string entityName)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, validationService, entityName)
    {

    }

    public bool ThrowExceptionFlag { get; set; } = false;

    public int ThrowExceptionCounter { get; set; }

    public bool ThrowNotImplementedExceptionFlag { get; set; } = false;

    public int ThrowNotImplementedExceptionCounter { get; set; }

    public int CreateAsyncCounter { get; set; }

    public int EditAsyncCounter { get; set; }

    public int ExistsAsyncCounter { get; set; }

    public bool ExistsAsyncResult { get; set; } = true;

    public int GetAllAsyncCounter { get; set; }

    public int GetAuthorIdAsyncCounter { get; set; }

    public int GetAllEntitiesByUserIdAsyncCounter { get; set; }

    public int GetAllEntitiesByPublisherIdAsyncCounter { get; set; }

    public int GetEntityDetailsAsyncCounter { get; set; }

    public int GetEntityInfoAsyncCounter { get; set; }

    protected override async Task<string> CreateAsync(BaseFormModel newEntity)
    {
        CreateAsyncCounter++;

        ThrowException();

        return await Task.FromResult("Success");
    }

    protected override async Task EditAsync(BaseFormModel updatedEntityFrom)
    {
        EditAsyncCounter++;

        ThrowException();

        await Task.CompletedTask;
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        ExistsAsyncCounter++;

        ThrowException();

        return await Task.FromResult(ExistsAsyncResult);
    }

    protected override async Task<BaseQueryModel<EmptyViewModel, Enum>> GetAllAsync(BaseQueryModel<EmptyViewModel, Enum> queryModel, string userId)
    {
        GetAllAsyncCounter++;

        ThrowException();

        return await Task.FromResult(new BaseQueryModel<EmptyViewModel, Enum>());
    }

    protected override async Task<IEnumerable<EmptyViewModel>> GetAllEntitiesByUserIdAsync(string userId)
    {
        GetAllEntitiesByUserIdAsyncCounter++;

        ThrowException();

        return await Task.FromResult(new List<EmptyViewModel>());
    }

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        GetAuthorIdAsyncCounter++;

        ThrowException();

        return await Task.FromResult("AuthorId");
    }

    protected override async Task<IEnumerable<EmptyViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        GetAllEntitiesByPublisherIdAsyncCounter++;

        ThrowException();

        return await Task.FromResult(new List<EmptyViewModel>());
    }

    protected override async Task<BaseDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        GetEntityDetailsAsyncCounter++;

        ThrowException();

        return await Task.FromResult(new BaseDetailsViewModel());
    }

    protected override async Task<BaseFormModel> GetEntityInfoAsync(string id)
    {
        GetEntityInfoAsyncCounter++;

        ThrowException();

        return await Task.FromResult(new BaseFormModel());
    }

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
}
