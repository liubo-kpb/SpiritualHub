namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;
using Client.Infrastructure.Extensions;
using Client.ViewModels.Event;

[Authorize]
public class EventController : Controller
{
    private readonly IEventService _eventService;
    private readonly ICategoryService _categoryService;

    public EventController(
        IEventService eventService,
        ICategoryService categoryService)
    {
        _eventService = eventService;
        _categoryService = categoryService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllEventsQueryModel queryModel)
    {
        var filteredEvents = await _eventService.GetAllAsync(queryModel, this.User.GetId()!);
        var categories = await _categoryService.GetAllAsync();

        queryModel.Events = filteredEvents.Events;
        queryModel.Categories = categories.Select(c => c.Name);
        queryModel.TotalEventsCount = filteredEvents.TotalEventsCount;

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Add(string id)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Add()
    {
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventFormModel updatedEvent)
    {
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> Delete()
    {
        return null;
    }
}
