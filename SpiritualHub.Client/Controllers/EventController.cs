namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;
using ViewModels.Event;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;

[Authorize]
public class EventController : Controller
{
    private readonly string entityName = nameof(Event).ToLower();

    private readonly IEventService _eventService;
    private readonly IPublisherService _publisherService;
    private readonly ICategoryService _categoryService;

    public EventController(
        IEventService eventService,
        IPublisherService publisherService,
        ICategoryService categoryService)
    {
        _eventService = eventService;
        _publisherService = publisherService;
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
    public async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsById(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        var eventForm = new EventFormModel()
        {
            Categories = await _categoryService.GetAllAsync(),
            Authors = await _publisherService.GetConnectedAuthorsAsync(userId),
        };

        if (!eventForm.Authors.Any())
        {
            TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

            return RedirectToAction(nameof(AuthorController.All), nameof(Author));
        }

        return View(eventForm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventFormModel newEvent)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            var eventModel = await _eventService.GetEventDetailsAsync(id, this.User.GetId()!);

            return View(eventModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"loading {entityName}");

            return RedirectToAction(nameof(All));
        }
        
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
