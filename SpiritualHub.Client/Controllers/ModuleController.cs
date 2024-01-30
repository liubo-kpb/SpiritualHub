namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ViewModels.BaseModels;
using ViewModels.Module;
using ViewModels.Course;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using SpiritualHub.Client.Infrastructure.Extensions;

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

    protected override async Task<string> CreateAsync(ModuleFormModel newEntity)
    {
        return await _moduleService.CreateAsync(newEntity);
    }

    protected override async Task EditAsync(ModuleFormModel updatedEntityFrom)
    {
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

    protected override Task<ModuleDetailsViewModule> GetEntityDetails(string id, string userId)
    {
        return _moduleService.GetModuleDetailsAsync(id, userId);
    }

    protected override Task<ModuleFormModel> GetEntityInfoAsync(string id)
    {
        return _moduleService.GetModuleInfoAsync(id);
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
        bool isUserConnectedPublisher = false;

        if (isUserLoggedIn)
        {
            string userId = this.User.GetId()!;
            bool isUserPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (isUserPublisher)
            {
                string authorId = await _moduleService.GetAuthorIdAsync(id);
                isUserConnectedPublisher = await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId);
            }
        }
        else
        {
            return AccessDeniedErrorMessage;
        }

        if (await _moduleService.IsActiveAsync(id)
            || (isUserLoggedIn && await UserHasAccess(id, isUserConnectedPublisher)))
        {
            return string.Empty;
        }

        return string.Format(NoEntityFoundErrorMessage, _entityName);
    }

    private async Task<bool> UserHasAccess(string id, bool isUserConnectedPublisher)
    {
        string courseId = await _moduleService.GetCourseIdAsync(id);

        return isUserConnectedPublisher
                || this.User.IsAdmin()
                || await _courseService.HasCourseAsync(courseId, this.User.GetId()!);
    }

    private IActionResult ReturnToHome()
    {
        TempData[ErrorMessage] = InvalidRequestErrorMessage;

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}
