namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;

using Services.Interfaces;
using Data.Models;
using ViewModels.Course;
using Infrastructure.Enums;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

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
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeHigherThanZeroErrorMessage);
        }

        await base.ValidateModelAsync(formModel, isUserAdmin);
    }
}
