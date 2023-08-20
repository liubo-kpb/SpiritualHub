﻿namespace SpiritualHub.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Home;
using Services.Interfaces;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IEventService _eventService;
    private readonly ICourseService _courseService;
    private readonly IBookService _bookService;
    private readonly IBlogService _blogService;
    private readonly IApplicationUserService _userService;

    public StatisticsController(IAuthorService authorService,
        IEventService eventService,
        ICourseService courseService,
        IBookService bookService,
        IBlogService blogService,
        IApplicationUserService userService)
    {
        _authorService = authorService;
        _eventService = eventService;
        _courseService = courseService;
        _bookService = bookService;
        _blogService = blogService;
        _userService = userService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(StatisticsViewModel))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetAllRessoursesCount()
    {
        try
        {
            StatisticsViewModel model = new StatisticsViewModel()
            {
                TotalActiveAuthors = await _authorService.GetAllCountAsync(),
                TotalEvents = await _eventService.GetAllCountAsync(),
                TotalCourses = await _courseService.GetAllCountAsync(),
                TotalBooks = await _bookService.GetAllCountAsync(),
                TotalBlogPosts = await _blogService.GetAllCountAsync(),
                TotalUsers = await _userService.GetAllCountAsync()
            };

            return Ok(model);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}