namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Services.Interfaces;
using Data.Models;
using ViewModels.Course;
using Infrastructure.Enums;
using Infrastructure.Extensions;

using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class CourseController : ProductController<CourseViewModel, CourseDetailsViewModel, CourseFormModel, AllCoursesQueryModel, CourseSorting>
{
    private readonly ICourseService _courseService;

    public CourseController(
        ICourseService courseService,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IServiceProvider serviceProvider)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, nameof(Course).ToLower())
    {
        _courseService = courseService;
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

    protected override async Task<IEnumerable<CourseViewModel>> GetAllEntitiesByUserIdAsync(string userId)
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

    protected override async Task GetAsync(string id, string userId)
    {
        await _courseService.GetAsync(id, userId);
    }

    protected override async Task RemoveAsync(string id, string userId)
    {
        await _courseService.RemoveAsync(id, userId);
    }

    protected override async Task DeleteAsync(string id)
    {
        await _courseService.DeleteAsync(id);
    }

    protected override async Task ShowAsync(string id)
    {
        await _courseService.ShowAsync(id);
    }

    protected override async Task HideAsync(string id)
    {
        await _courseService.HideAsync(id);
    }

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        return await _courseService.GetAuthorIdAsync(entityId);
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        return await _courseService.HasCourseAsync(id, usedId);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        return AlreadyHasCourseErrorMessage;
    }

    protected override string GetEntitySuccessMessage()
    {
        return GetCourseSuccessMessage;
    }

    protected override string RemoveEntitySuccessMessage()
    {
        return RemoveCourseSuccessMessage;
    }

    protected override async Task ValidateModelAsync(CourseFormModel formModel)
    {
        if (formModel.Price < 0)
        {
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeZeroOrHigherErrorMessage);
        }

        await base.ValidateModelAsync(formModel);
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
                isUserConnectedPublisher = await _publisherService.IsConnectedToAuthorByUserId(userId, authorId);
            }
        }

        if (await _courseService.IsActiveAsync(id)
            || (isUserLoggedIn && await UserHasAccess(id, isUserConnectedPublisher)))
        {
            return string.Empty;
        }

        return string.Format(NoEntityFoundErrorMessage, _entityName);
    }

    private async Task<bool> UserHasAccess(string id, bool isUserConnectedPublisher)
    {
        return isUserConnectedPublisher
                || this.User.IsAdmin()
                || await _courseService.HasCourseAsync(id, this.User.GetId()!);
    }
}
