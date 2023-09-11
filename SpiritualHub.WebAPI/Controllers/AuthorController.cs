namespace SpiritualHub.WebAPI.Controllers;

using Microsoft.AspNetCore.Mvc;

using Responses;
using Client.ViewModels.Event;
using Services.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;

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
}