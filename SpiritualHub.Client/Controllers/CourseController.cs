namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;
using Data.Models;
using ViewModels.Course;
using Infrastructure.Extensions;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using SpiritualHub.Services;

public class CourseController : Controller
{
    private readonly string entityName = nameof(Course).ToLower();

    private readonly ICourseService _courseService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;
    public CourseController(
        ICourseService courseService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
    {
        _courseService = courseService;
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
            var filteredBooks = await _courseService.GetAllAsync(queryModel, this.User.GetId()!);
            var categories = await _categoryService.GetAllAsync();

            queryModel.Courses = filteredBooks.Courses;
            queryModel.Categories = categories.Select(c => c.Name);
            queryModel.TotalCoursesCount = filteredBooks.TotalCoursesCount;

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

        return View();
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
}
