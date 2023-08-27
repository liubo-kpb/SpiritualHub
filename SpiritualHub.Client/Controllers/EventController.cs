namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }
}
