namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;
using Data.Models;
using ViewModels.Course;
using ViewModels.Publisher;
using Infrastructure.Extensions;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class CourseController : Controller
{
    private readonly string entityName = nameof(Course).ToLower();

    private readonly ICourseService _courseService;
    private readonly IModuleService _moduleService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;

    public CourseController(
        ICourseService courseService,
        IModuleService moduleService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
    {
        _courseService = courseService;
        _moduleService = moduleService;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllCoursesQueryModel queryModel)
    {
        try
        {
            var filteredCourses = await _courseService.GetAllAsync(queryModel, this.User.GetId()!);
            var categories = await _categoryService.GetAllAsync();

            queryModel.Courses = filteredCourses.Courses;
            queryModel.Categories = categories.Select(c => c.Name);
            queryModel.TotalCoursesCount = filteredCourses.TotalCoursesCount;

            return View(queryModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}s");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _courseService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            var courseModel = await _courseService.GetCourseDetailsAsync(id, this.User.GetId()!);

            return View(courseModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"loading {entityName}");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        try
        {
            var coursesModel = await _courseService.AllCoursesByUserIdAsync(this.User.GetId()!);

            return View(coursesModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> MyPublishings()
    {
        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;
            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var publisherId = await _publisherService.GetPublisherIdAsync(userId);
            var coursesModel = await _courseService.GetCoursesByPublisherIdAsync(publisherId, userId);

            return View(coursesModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        bool isPublisher = isUserAdmin || await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        var formModel = new CourseFormModel();

        await GetCourseFormDetailsAsync(formModel, userId, isUserAdmin);
        if (!formModel.Authors.Any())
        {
            TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

            return RedirectToAction(nameof(AuthorController.All), nameof(Author));
        }

        return View(formModel);
    }

    private async Task GetCourseFormDetailsAsync(CourseFormModel courseFormModel, string userId, bool isUserAdmin = false)
    {
        if (isUserAdmin)
        {
            courseFormModel.Authors = await _authorService.GetAllAsync();

            if (courseFormModel.AuthorId == null)
            {
                courseFormModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                courseFormModel.Publishers = await _authorService.GetConnectedEntities<Publisher, PublisherInfoViewModel>(courseFormModel.AuthorId);
            }
        }
        else
        {
            courseFormModel.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
        }
        if (courseFormModel.Id != null)
        {
            courseFormModel.Modules = await _moduleService.GetModulesByCourseId(courseFormModel.Id);
        }

        courseFormModel.Categories = await _categoryService.GetAllAsync();
    }
}
