namespace SpiritualHub.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using Responses;
using Client.ViewModels.Event;
using Client.ViewModels.Book;
using Services.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;
using SpiritualHub.Client.ViewModels.Subscription;

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
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));
            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntities<Event, EventInfoViewModel>(id))!
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
    [Route("{id}/books")]
    [Produces("application/json")]
    [ProducesResponseType(200, Type = typeof(CollectionResponse<BookInfoViewModel>))]
    [ProducesResponseType(204, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetConnectedBooks(string id)
    {
        var response = new CollectionResponse<BookInfoViewModel>();
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));
            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntities<Book, BookInfoViewModel>(id))!
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
        bool exists = await _authorService.Exists(id);
        if (!exists)
        {
            response.AddError(string.Format(NoEntityFoundErrorMessage, entityName));
            return NotFound(response.Error);
        }

        try
        {
            response.Data = (await _authorService.GetConnectedEntities<Subscription, SubscriptionViewModel>(id))!.OrderBy(s => s.Price);

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
}