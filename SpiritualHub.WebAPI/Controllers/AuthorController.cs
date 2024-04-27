namespace SpiritualHub.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using Responses;
using Client.ViewModels.Event;
using Client.ViewModels.Book;
using Client.ViewModels.Course;
using Client.ViewModels.Subscription;
using Services.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;

[ApiController]
[Route("api/author")]
public class AuthorController : ControllerBase
{
    private readonly string entityName = nameof(Author).ToLower();

    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    [Route("{id}/events")]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(CollectionResponse<EventInfoViewModel>))]
    [ProducesResponseType(204, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetConnectedEvents(string id)
    {
        var response = new CollectionResponse<EventInfoViewModel>();
        bool exists = await _authorService.ExistsAsync(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));

            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntitiesAsync<Event, EventInfoViewModel>(id))!
                                                           .Where(e => e.StartDateTime > DateTime.Now.Date);

            if (!response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }
        catch (Exception)
        {
            response.AddError(string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}'s events on the server"));

            return BadRequest(response.Error);
        }
    }

    [HttpGet]
    [Route("{id}/courses")]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(CollectionResponse<CourseInfoViewModel>))]
    [ProducesResponseType(204, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetConnectedCourses(string id)
    {
        var response = new CollectionResponse<CourseInfoViewModel>();
        bool exists = await _authorService.ExistsAsync(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));

            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntitiesAsync<Course, CourseInfoViewModel>(id))!
                                                                                .Where(c => c.IsActive);

            if (!response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }
        catch (Exception)
        {
            response.AddError(string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}'s courses on the server"));

            return BadRequest(response.Error);
        }
    }

    [HttpGet]
    [Route("{id}/books")]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(CollectionResponse<BookInfoViewModel>))]
    [ProducesResponseType(204, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetConnectedBooks(string id)
    {
        var response = new CollectionResponse<BookInfoViewModel>();
        bool exists = await _authorService.ExistsAsync(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));

            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntitiesAsync<Book, BookInfoViewModel>(id))!
                                                                            .Where(e => !e.IsHidden);

            if (!response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }
        catch (Exception)
        {
            response.AddError(string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}'s books on the server"));

            return BadRequest(response.Error);
        }
    }

    [HttpGet]
    [Route("{id}/subscriptions")]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(CollectionResponse<SubscriptionViewModel>))]
    [ProducesResponseType(204, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetConnectedSubscriptoins(string id)
    {
        var response = new CollectionResponse<SubscriptionViewModel>();
        bool exists = await _authorService.ExistsAsync(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));

            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntitiesAsync<Subscription, SubscriptionViewModel>(id))!
                                                                                        .OrderBy(s => s.Price);

            if (!response.Data.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }
        catch (Exception)
        {
            response.AddError(string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}'s subscriptions on the server"));

            return BadRequest(response.Error);
        }
    }

}