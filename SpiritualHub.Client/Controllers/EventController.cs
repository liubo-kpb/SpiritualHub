namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;
using ViewModels.Event;
using ViewModels.Publisher;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
public class EventController : Controller
{
    private readonly string entityName = nameof(Event).ToLower();

    private readonly IEventService _eventService;
    private readonly IPublisherService _publisherService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;

    public EventController(
        IEventService eventService,
        IPublisherService publisherService,
        IAuthorService authorService,
        ICategoryService categoryService)
    {
        _eventService = eventService;
        _publisherService = publisherService;
        _authorService = authorService;
        _categoryService = categoryService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllEventsQueryModel queryModel)
    {
        try
        {
            var filteredEvents = await _eventService.GetAllAsync(queryModel, this.User.GetId()!);
            var categories = await _categoryService.GetAllAsync();

            queryModel.Events = filteredEvents.Events;
            queryModel.Categories = categories.Select(c => c.Name);
            queryModel.TotalEventsCount = filteredEvents.TotalEventsCount;

            return View(queryModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}s");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        bool isPublisher = isUserAdmin ? true : await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        var eventForm = new EventFormModel()
        {
            StartDateTime = DateTime.Now.Date,
            EndDateTime = DateTime.Now.Date,
        };

        await GetEventFormDetailsAsync(eventForm, userId, isUserAdmin);
        if (!eventForm.Authors.Any())
        {
            TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

            return RedirectToAction(nameof(AuthorController.All), nameof(Author));
        }

        return View(eventForm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventFormModel newEventForm)
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();

        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = await _publisherService.IsConnectedToEntityByUserId<Author>(userId, newEventForm.AuthorId);
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(newEventForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(newEventForm, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetEventFormDetailsAsync(newEventForm, userId, isUserAdmin);

            return View(newEventForm);
        }

        try
        {
            newEventForm.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            string id = await _eventService.CreateAsync(newEventForm);
            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create {entityName}");

            await GetEventFormDetailsAsync(newEventForm, userId, isUserAdmin);

            return View(newEventForm);
        }
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
        try
        {
            var eventFormModel = await _eventService.GetEventInfoAsync(id);
            if (eventFormModel == null)
            {
                TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

                return RedirectToAction(nameof(All));
            }

            string userId = this.User.GetId()!;
            bool isUserAdmin = this.User.IsAdmin();

            if (!isUserAdmin)
            {
                bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
                if (!isPublisher)
                {
                    TempData[ErrorMessage] = NotAPublisherErrorMessage;

                    return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
                }

                bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, eventFormModel.AuthorId))
                                         && (await _publisherService.IsConnectedToEntityByUserId<Event>(userId, eventFormModel.Id!));
                if (!isConnectedPublisher)
                {
                    TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author and {entityName}");

                    return RedirectToAction(nameof(MyPublishings));
                }
            }

            await GetEventFormDetailsAsync(eventFormModel, userId, isUserAdmin);

            return View(eventFormModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EventFormModel updatedEventForm)
    {
        bool exists = await _eventService.ExistsAsync(updatedEventForm.Id!);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, updatedEventForm.AuthorId))
                                     && (await _publisherService.IsConnectedToEntityByUserId<Event>(userId, updatedEventForm.Id!));
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(updatedEventForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(updatedEventForm, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetEventFormDetailsAsync(updatedEventForm, userId, isUserAdmin);

            return View(updatedEventForm);
        }

        try
        {
            updatedEventForm.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            await _eventService.EditAsync(updatedEventForm);
            TempData[SuccessMessage] = string.Format(EditSuccessfulMessage, entityName);

            return RedirectToAction(nameof(Details), new { id = updatedEventForm.Id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"edit {entityName}");

            await GetEventFormDetailsAsync(updatedEventForm, userId, isUserAdmin);

            return View(updatedEventForm);
        }
    }


    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        if (!this.User.IsAdmin())
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId))
                                     && (await _publisherService.IsConnectedToEntityByUserId<Event>(userId, id));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author and {entityName}");

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            var eventModel = await _eventService.GetEventInfoAsync(id);

            return View(eventModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(EventDetailsViewModel eventModel)
    {
        bool exists = await _eventService.ExistsAsync(eventModel.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        if (!this.User.IsAdmin())
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(eventModel.Id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId))
                                     && (await _publisherService.IsConnectedToEntityByUserId<Event>(userId, eventModel.Id));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author and {entityName}");

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _eventService.DeleteAsync(eventModel.Id);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"deleting the {entityName}");

            return View(new { id = eventModel.Id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        try
        {
            var eventsModel = await _eventService.AllEventsByUserIdAsync(this.User.GetId()!);

            return View(eventsModel);
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
            var eventsModel = await _eventService.GetEventsByPublisherIdAsync(publisherId);

            return View(eventsModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}s");

            return RedirectToAction(nameof(All));
        }

    }

    [HttpPost]
    public async Task<IActionResult> Join(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        if (!this.User.IsAdmin())
        {
            bool isJoined = await _eventService.IsJoinedAsync(id, userId);
            if (isJoined)
            {
                TempData[ErrorMessage] = AlreadyJoinedErrorMessage;

                return RedirectToAction(nameof(All));
            }
        }

        try
        {
            await _eventService.JoinAsync(id, userId);
            TempData[SuccessMessage] = JoinEventSuccessfulMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"join the {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Leave(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _eventService.LeaveAsync(id, this.User.GetId()!);
            TempData[SuccessMessage] = LeaveEventSuccessfulMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"leave the {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    private async Task GetEventFormDetailsAsync(EventFormModel eventFormModel, string userId, bool isUserAdmin = false)
    {
        if (isUserAdmin)
        {
            eventFormModel.Authors = await _authorService.GetAllAsync();

            if (eventFormModel.AuthorId == null)
            {
                eventFormModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                eventFormModel.Publishers = await _authorService.GetConnectedEntities<Publisher, PublisherInfoViewModel>(eventFormModel.AuthorId);
            }
        }
        else
        {
            eventFormModel.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
        }
        eventFormModel.Categories = await _categoryService.GetAllAsync();
    }

    private async Task ValidateModelAsync(EventFormModel eventForm, bool isUserAdmin)
    {
        if (eventForm.Price < 0)
        {
            ModelState.AddModelError(nameof(eventForm.Price), PriceMustBeHigherThanZeroErrorMessage);
        }

        if (eventForm.StartDateTime < DateTime.Now)
        {
            ModelState.AddModelError(nameof(eventForm.StartDateTime), string.Format(WrongDateErrorMessage, "Start date", "today's date"));
        }

        if (eventForm.StartDateTime > eventForm.EndDateTime)
        {
            ModelState.AddModelError(nameof(eventForm.EndDateTime), string.Format(WrongDateErrorMessage, "End date", "start date"));
        }

        bool isExistingAuthor = await _authorService.Exists(eventForm.AuthorId);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(eventForm.CategoryId), string.Format(NoEntityFoundErrorMessage, "author"));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(eventForm.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(eventForm.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }

        if (!eventForm.IsOnline &&
            (string.IsNullOrEmpty(eventForm.LocationName) || string.IsNullOrEmpty(eventForm.LocationUrl)))
        {
            ModelState.AddModelError(nameof(eventForm.IsOnline), string.Format(SpecifyParticipationErrorMessage));
            ModelState.AddModelError(nameof(eventForm.LocationName), string.Format(SpecifyParticipationErrorMessage));
            ModelState.AddModelError(nameof(eventForm.LocationUrl), string.Format(SpecifyParticipationErrorMessage));
        }

        if (isUserAdmin)
        {
            if (!(await _publisherService.ExistsByIdAsync(eventForm.PublisherId!)))
            {
                ModelState.AddModelError(nameof(eventForm.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
            }
            else if (!(await _publisherService.IsConnectedToEntityByPublisherId<Author>(eventForm.PublisherId!, eventForm.AuthorId)))
            {
                ModelState.AddModelError(nameof(eventForm.PublisherId), WrongPublisherErrorMessage);
            }
        }
    }
}