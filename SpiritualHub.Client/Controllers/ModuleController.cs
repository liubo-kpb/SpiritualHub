﻿namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ViewModels.BaseModels;
using ViewModels.Module;
using ViewModels.Course;
using Infrastructure.Extensions;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class ModuleController : BaseController<EmptyViewModel, ModuleDetailsViewModule, ModuleFormModel, EmptyQueryModel, Enum>
{
    private readonly IModuleService _moduleService;
    private readonly ICourseService _courseService;

    private string _newModuleCourseId = null!;

    public ModuleController(
        IModuleService moduleService,
        ICourseService courseService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
        : base(authorService, categoryService, publisherService, nameof(Module).ToLower())
    {
        _moduleService = moduleService;
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> Append(string courseId)
    {
        _newModuleCourseId = courseId;

        return await Add();
    }

    public override async Task<IActionResult> All([FromQuery] EmptyQueryModel queryModel)
    {
        return ReturnToHome();
    }

    public override async Task<IActionResult> Mine()
    {
        return ReturnToHome();
    }

    public override async Task<IActionResult> MyPublishings()
    {
        return ReturnToHome();
    }

    [HttpPost]
    public async Task<IActionResult> Hide(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _courseService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _moduleService.HideAsync(id);
            TempData[SuccessMessage] = string.Format(HideEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"hide the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Show(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _courseService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _moduleService.ShowAsync(id);
            TempData[SuccessMessage] = string.Format(ShowEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"show the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    protected override async Task<string> CreateAsync(ModuleFormModel newEntity)
    {
        await _moduleService.AdjustModulesNumbering(newEntity, true);
        return await _moduleService.CreateAsync(newEntity);
    }

    protected override async Task EditAsync(ModuleFormModel updatedEntityFrom)
    {
        await _moduleService.AdjustModulesNumbering(updatedEntityFrom);
        await _moduleService.EditAsync(updatedEntityFrom);
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _moduleService.ExistsAsync(id);
    }

    protected override Task<EmptyQueryModel> GetAllAsync(EmptyQueryModel queryModel, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetAllEntitiesByUserId(string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override async Task<ModuleDetailsViewModule> GetEntityDetails(string id, string userId)
    {
        var moduleViewModel = await _moduleService.GetModuleDetailsAsync(id, userId);
        
        bool canModify = await CanModify();
        moduleViewModel.NextModuleId = _moduleService.GetNextModuleId(moduleViewModel, canModify)!;
        moduleViewModel.PreviousModuleId = _moduleService.GetPreviousModuleId(moduleViewModel, canModify)!;

        return moduleViewModel;
    }

    protected override async Task<ModuleFormModel> GetEntityInfoAsync(string id)
    {
        return await _moduleService.GetModuleInfoAsync(id);
    }

    protected override async Task GetFormDetailsAsync(ModuleFormModel formModel, string userId, bool isUserAdmin = false)
    {
        formModel.AuthorId ??= await _courseService.GetAuthorIdAsync(formModel.CourseId);
        formModel.Courses = await _authorService.GetConnectedEntities<Course, CourseInfoViewModel>(formModel.AuthorId!);
    }

    protected override async Task ValidateModelAsync(ModuleFormModel formModel, bool isUserAdmin)
    {
        bool isExistingAuthor = await _authorService.ExistsAsync(formModel.AuthorId!);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(formModel.AuthorId), string.Format(NoEntityFoundErrorMessage, "author"));
        }

        bool isExistingCourse = await _courseService.ExistsAsync(formModel.CourseId);
        if (!isExistingCourse)
        {
            ModelState.AddModelError(nameof(formModel.CourseId), string.Format(NoEntityFoundErrorMessage, "course"));
        }

        if (formModel.VideoUrl?.Contains("youtu.be") ?? false)
        {
            ModelState.AddModelError(nameof(formModel.VideoUrl), UserRegularVideoUrlErrorMessage);
        }

        formModel.PublisherId = string.Empty;
    }

    protected override ModuleFormModel CreateFormModelInstance()
    {
        if (string.IsNullOrEmpty(_newModuleCourseId))
        {
            throw new NotImplementedException(InvalidRequestErrorMessage);
        }

        return new ModuleFormModel()
        {
            CourseId = _newModuleCourseId
        };
    }

    protected override async Task<string?> CustomValidateAsync(string id)
    {
        bool isUserLoggedIn = this.User.Identity?.IsAuthenticated ?? false;

        if (isUserLoggedIn)
        {
            if (await _moduleService.IsActiveAsync(id)
                || await UserHasAccess(id))
            {
                return string.Empty;
            }
        }
        else
        {
            return AccessDeniedErrorMessage;
        }


        return string.Format(NoEntityFoundErrorMessage, _entityName);
    }

    private async Task<bool> UserHasAccess(string id)
    {
        string courseId = await _moduleService.GetCourseIdAsync(id);

        return await CanModify()
                || await _courseService.HasCourseAsync(courseId, this.User.GetId()!);
    }

    private async Task<bool> CanModify()
    {
        return this.User.IsAdmin()
                || await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
    }
}
