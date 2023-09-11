namespace SpiritualHub.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.Home;
using Services.Interfaces;
using SpiritualHub.WebAPI.Responses;

using static Common.ErrorMessagesConstants;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IEventService _eventService;
    private readonly ICourseService _courseService;
    private readonly IBookService _bookService;
    private readonly IBlogService _blogService;
    private readonly IUserService _userService;

    public StatisticsController(
        IAuthorService authorService,
        IEventService eventService,
        ICourseService courseService,
        IBookService bookService,
        IBlogService blogService,
        IUserService userService)
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
        var response = new SingleResponse<StatisticsViewModel>();
        try
        {
            response.Model = new StatisticsViewModel()
            {
                TotalActiveAuthors = await _authorService.GetAllCountAsync(),
                TotalEvents = await _eventService.GetAllCountAsync(),
                TotalCourses = await _courseService.GetAllCountAsync(),
                TotalBooks = await _bookService.GetAllCountAsync(),
                TotalBlogPosts = await _blogService.GetAllCountAsync(),
                TotalUsers = await _userService.GetAllCountAsync()
            };

            return Ok(response);
        }
        catch (Exception)
        {
            response.AddError(string.Format(GeneralUnexpectedErrorMessage, "load statistics on the server"));

            return BadRequest(response.Error);
        }
    }
}
