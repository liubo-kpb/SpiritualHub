namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using ViewModels.BaseModels;
using ViewModels.Module;
using ViewModels.Course;
using Infrastructure.Extensions;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;

public class ModuleController : ProductController<EmptyViewModel, ModuleDetailsViewModule, ModuleFormModel, EmptyQueryModel, Enum>
{
    private readonly IModuleService _moduleService;
    private readonly ICourseService _courseService;

    private string _newModuleCourseId = null!;

    public ModuleController(
        IModuleService moduleService,
        ICourseService courseService,
        IServiceProvider serviceProvider,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, nameof(Module).ToLower())
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

    public override async Task<IActionResult> Delete(ModuleDetailsViewModule detailsViewModel)
    {
        var result = await base.Delete(detailsViewModel);
        if (IsRedirectToMyPublishings(result) && TempData[ErrorMessage] == null)
        {
            return RedirectToAction(nameof(Details), "Course", new { id = detailsViewModel.CourseId });
        }

        return result;
    }

    public override async Task<IActionResult> Show(string id)
    {
        var result = await base.Show(id);
        if (IsRedirectToMyPublishings(result) && TempData[ErrorMessage] == null)
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        return result;
    }

    public override async Task<IActionResult> Hide(string id)
    {
        var result = await base.Hide(id);
        if (IsRedirectToMyPublishings(result) && TempData[ErrorMessage] == null)
        {
            return RedirectToAction(nameof(Details), new { id });
        }

        return result;
    }

    public override async Task<IActionResult> All([FromQuery] EmptyQueryModel queryModel)
    {
        return RedirectToAction(nameof(All), "Course");
    }

    public override async Task<IActionResult> Mine()
    {
        return RedirectToAction(nameof(Mine), "Course");
    }

    public override async Task<IActionResult> MyPublishings()
    {
        return RedirectToAction(nameof(MyPublishings), "Course");
    }

    public override async Task<IActionResult> Get(string id)
    {
        TempData[ErrorMessage] = InvalidRequestErrorMessage;

        return RedirectToAction(nameof(Details), new { id });
    }

    public override async Task<IActionResult> Remove(string id)
    {
        TempData[ErrorMessage] = InvalidRequestErrorMessage;

        return RedirectToAction(nameof(Details), new { id });
    }

    protected override async Task<string> CreateAsync(ModuleFormModel newEntity)
    {
        await _moduleService.AdjustModulesNumberingAsync(newEntity, true);
        return await _moduleService.CreateAsync(newEntity);
    }

    protected override async Task EditAsync(ModuleFormModel updatedEntityFrom)
    {
        await _moduleService.AdjustModulesNumberingAsync(updatedEntityFrom);
        await _moduleService.EditAsync(updatedEntityFrom);
    }

    protected override async Task<ModuleDetailsViewModule> GetEntityDetails(string id, string userId)
    {
        var moduleViewModel = await _moduleService.GetModuleDetailsAsync(id);

        bool canModify = await CanModify(moduleViewModel.AuthorId);
        moduleViewModel.NextModuleId = _moduleService.GetNextModuleId(moduleViewModel, canModify)!;
        moduleViewModel.PreviousModuleId = _moduleService.GetPreviousModuleId(moduleViewModel, canModify)!;

        return moduleViewModel;
    }

    protected override async Task<ModuleFormModel> GetEntityInfoAsync(string id)
    {
        return await _moduleService.GetModuleInfoAsync(id);
    }

    protected override async Task DeleteAsync(string id)
    {
        await _moduleService.DeleteAsync(id);
    }

    protected override async Task ShowAsync(string id)
    {
        await _moduleService.ShowAsync(id);
    }

    protected override async Task HideAsync(string id)
    {
        await _moduleService.HideAsync(id);
    }

    protected override async Task GetFormDetailsAsync(ModuleFormModel formModel, bool callBase = false)
    {
        formModel.AuthorId ??= await _courseService.GetAuthorIdAsync(formModel.CourseId);
        formModel.Courses = await _authorService.GetConnectedEntitiesAsync<Course, CourseInfoViewModel>(formModel.AuthorId!);
    }

    protected override async Task ValidateModelAsync(ModuleFormModel formModel)
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

    protected override async Task<string> CustomValidateAsync(string id)
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

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        return await _courseService.GetAuthorIdAsync(await GetCourseIdAsync(entityId));
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        return await _courseService.HasCourseAsync(await GetCourseIdAsync(id), usedId);
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _moduleService.ExistsAsync(id);
    }

    private async Task<bool> UserHasAccess(string id)
    {
        return await _courseService.HasCourseAsync(await GetCourseIdAsync(id), GetUserId()!)
            || await ValidateModifyPermissionsAsync(await _moduleService.GetAuthorIdAsync(id));
    }

    private async Task<string> GetCourseIdAsync(string moduleId)
    {
        return await _moduleService.GetCourseIdAsync(moduleId);
    }

    private bool IsRedirectToMyPublishings(IActionResult result)
    {
        var redirect = result as RedirectToActionResult;

        return redirect!.ActionName == nameof(MyPublishings);
    }

    protected override Task<EmptyQueryModel> GetAllAsync(EmptyQueryModel queryModel, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetAllEntitiesByUserIdAsync(string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task<IEnumerable<EmptyViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task GetAsync(string id, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task RemoveAsync(string id, string userId)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override string GetEntitySuccessMessage()
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override string RemoveEntitySuccessMessage()
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }
}
