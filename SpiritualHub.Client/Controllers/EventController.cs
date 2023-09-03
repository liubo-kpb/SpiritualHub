namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;
using ViewModels.Author;
using ViewModels.Event;
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
        bool isPublisher = this.User.IsAdmin() ? true : await _publisherService.ExistsByUserId(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        IEnumerable<AuthorInfoViewModel> authors = new List<AuthorInfoViewModel>();
        if (this.User.IsAdmin())
        {
            authors = await _authorService.GetAllAsync();
        }
        else
        {
            authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            if (!authors.Any())
            {
                TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

                return RedirectToAction(nameof(AuthorController.All), nameof(Author));
            }
        }

        var eventForm = new EventFormModel()
        {
            Categories = await _categoryService.GetAllAsync(),
            Authors = authors,
            Publishers = await _publisherService.GetAllAsync(),
            StartDateTime = DateTime.Now.Date,
            EndDateTime = DateTime.Now.Date,
        };

        return View(eventForm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(EventFormModel newEventForm)
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();

        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserId(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = await _publisherService.IsConnectedToEntity<Author>(userId, newEventForm.AuthorId);
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(newEventForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(newEventForm);
        if (!ModelState.IsValid)
        {
            if (isUserAdmin)
            {
                newEventForm.Authors = await _authorService.GetAllAsync();
                newEventForm.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                newEventForm.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            }
            newEventForm.Categories = await _categoryService.GetAllAsync();

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

            if (isUserAdmin)
            {
                newEventForm.Authors = await _authorService.GetAllAsync();
                newEventForm.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                newEventForm.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            }
            newEventForm.Categories = await _categoryService.GetAllAsync();

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
                bool isPublisher = await _publisherService.ExistsByUserId(userId);
                if (!isPublisher)
                {
                    TempData[ErrorMessage] = NotAPublisherErrorMessage;

                    return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
                }

                bool isConnectedPublisher = (await _publisherService.IsConnectedToEntity<Author>(userId, eventFormModel.AuthorId))
                                         && (await _publisherService.IsConnectedToEntity<Event>(userId, eventFormModel.Id.ToString()));
                if (!isConnectedPublisher)
                {
                    TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author and {entityName}");

                    return RedirectToAction(nameof(MyPublishings));
                }
            }

            if (isUserAdmin)
            {
                eventFormModel.Authors = await _authorService.GetAllAsync();
                eventFormModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                eventFormModel.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            }
            eventFormModel.Categories = await _categoryService.GetAllAsync();

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
        bool exists = await _eventService.ExistsAsync(updatedEventForm.Id.ToString());
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserId(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntity<Author>(userId, updatedEventForm.AuthorId))
                                     && (await _publisherService.IsConnectedToEntity<Event>(userId, updatedEventForm.Id.ToString()));
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(updatedEventForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(updatedEventForm);
        if (!ModelState.IsValid)
        {
            if (isUserAdmin)
            {
                updatedEventForm.Authors = await _authorService.GetAllAsync();
                updatedEventForm.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                updatedEventForm.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            }
            updatedEventForm.Categories = await _categoryService.GetAllAsync();

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

            if (isUserAdmin)
            {
                updatedEventForm.Authors = await _authorService.GetAllAsync();
                updatedEventForm.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                updatedEventForm.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
            }
            updatedEventForm.Categories = await _categoryService.GetAllAsync();

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
            bool isPublisher = await _publisherService.ExistsByUserId(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntity<Author>(userId, authorId))
                                     && (await _publisherService.IsConnectedToEntity<Event>(userId, id));
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

    [HttpPost]
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
            bool isPublisher = await _publisherService.ExistsByUserId(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(eventModel.Id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntity<Author>(userId, authorId))
                                     && (await _publisherService.IsConnectedToEntity<Event>(userId, eventModel.Id));
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
        bool isPublisher = await _publisherService.ExistsByUserId(userId);
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

    private async Task ValidateModelAsync(EventFormModel eventForm)
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

        if (this.User.IsAdmin() && !(await _publisherService.ExistsById(eventForm.PublisherId!)))
        {
            ModelState.AddModelError(nameof(eventForm.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
        }
    }
}
