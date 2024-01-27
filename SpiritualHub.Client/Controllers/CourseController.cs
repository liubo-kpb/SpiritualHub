namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;
using Data.Models;
using ViewModels.Course;
using Infrastructure.Enums;
using Infrastructure.Extensions;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using SpiritualHub.Services;

public class CourseController : BaseController<CourseViewModel, CourseDetailsViewModel, CourseFormModel, AllCoursesQueryModel, CourseSorting>
{
    private readonly ICourseService _courseService;

    public CourseController(
        ICourseService courseService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
        : base(authorService, categoryService, publisherService, nameof(Course).ToLower())
    {
        _courseService = courseService;
    }

    [HttpPost]
    public async Task<IActionResult> Get(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool hasCourse = await _courseService.HasCourseAsync(id, userId);
        if (hasCourse)
        {
            TempData[ErrorMessage] = AlreadyHasCourseErrorMessage;

            return RedirectToAction(nameof(Details), new { id });
        }

        try
        {
            await _courseService.GetAsync(id, userId);
            TempData[SuccessMessage] = GetCourseSuccessMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"purchase {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Remove(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _courseService.RemoveAsync(id, this.User.GetId()!);
            TempData[SuccessMessage] = RemoveCourseSuccessMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"remove {_entityName} from your library");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
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
            var courseModel = await GetEntityInfoAsync(id);

            return View(courseModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(CourseDetailsViewModel courseModel)
    {
        bool exists = await ExistsAsync(courseModel.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        bool isPublisher = this.User.IsAdmin() ? true : await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            await _courseService.DeleteAsync(courseModel.Id);
            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"delete {_entityName}");

            return View(new { id = courseModel.Id });
        }
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
            await _courseService.HideAsync(id);
            TempData[SuccessMessage] = string.Format(HideEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
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
            await _courseService.ShowAsync(id);
            TempData[SuccessMessage] = string.Format(ShowEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"show the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    protected override async Task<string> CreateAsync(CourseFormModel newEntity)
    {
        return await _courseService.CreateAsync(newEntity);
    }

    protected override async Task EditAsync(CourseFormModel updatedEntityFrom)
    {
        await _courseService.EditAsync(updatedEntityFrom);
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _courseService.ExistsAsync(id);
    }

    protected override async Task<AllCoursesQueryModel> GetAllAsync(AllCoursesQueryModel queryModel, string userId)
    {
        var filteredCourses = await _courseService.GetAllAsync(queryModel, userId);

        queryModel.EntityViewModels = filteredCourses.Courses;
        queryModel.TotalEntitiesCount = filteredCourses.TotalCoursesCount;

        return queryModel;
    }

    protected override async Task<IEnumerable<CourseViewModel>> GetAllEntitiesByUserId(string userId)
    {
        return await _courseService.AllCoursesByUserIdAsync(userId);
    }

    protected override async Task<IEnumerable<CourseViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        return await _courseService.GetCoursesByPublisherIdAsync(publisherId, userId);
    }

    protected override async Task<CourseDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        return await _courseService.GetCourseDetailsAsync(id, userId);
    }

    protected override async Task<CourseFormModel> GetEntityInfoAsync(string id)
    {
        return await _courseService.GetCourseInfoAsync(id);
    }

    protected override async Task ValidateModelAsync(CourseFormModel formModel, bool isUserAdmin)
    {
        if (formModel.Price < 0)
        {
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeZeroOrHigherErrorMessage);
        }

        await base.ValidateModelAsync(formModel, isUserAdmin);
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
                string authorId = await _courseService.GetAuthorIdAsync(id);
                isUserConnectedPublisher = await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId);
            }
        }

        if (await _courseService.IsActiveAsync(id)
            || (isUserLoggedIn && await UserHasAccess(id, isUserConnectedPublisher)))
        {
            return null!;
        }

        return string.Format(NoEntityFoundErrorMessage, _entityName);
    }

    private async Task<bool> UserHasAccess(string id, bool isUserConnectedPublisher)
    {
        return this.User.IsAdmin()
                || isUserConnectedPublisher
                || await _courseService.HasCourseAsync(id, this.User.GetId()!);
    }
}
