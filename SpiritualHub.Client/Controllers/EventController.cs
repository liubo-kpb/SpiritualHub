namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Infrastructure.Enums;
using Infrastructure.Extensions;
using ViewModels.Event;
using Services.Interfaces;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class EventController : BaseController<EventViewModel, EventDetailsViewModel, EventFormModel, AllEventsQueryModel, EventSorting>
{
    private readonly IEventService _eventService;

    public EventController(
        IEventService eventService,
        IPublisherService publisherService,
        IAuthorService authorService,
        ICategoryService categoryService)
        : base (authorService, categoryService, publisherService, nameof(Event).ToLower())
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }

    }

    [HttpPost]
    public async Task<IActionResult> Delete(EventDetailsViewModel eventModel)
    {
        bool exists = await _eventService.ExistsAsync(eventModel.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _eventService.GetAuthorIdAsync(eventModel.Id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = NotAConnectedPublisherErrorMessage;

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _eventService.DeleteAsync(eventModel.Id);
            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"deleting the {_entityName}");

            return View(new { id = eventModel.Id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Join(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"join the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Leave(string id)
    {
        bool exists = await _eventService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"leave the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    protected override async Task<string> CreateAsync(EventFormModel newEntity)
    {
        return await _eventService.CreateAsync(newEntity);
    }

    protected override async Task EditAsync(EventFormModel updatedEntityFrom)
    {
        await _eventService.EditAsync(updatedEntityFrom);
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _eventService.ExistsAsync(id);
    }

    protected override async Task<AllEventsQueryModel> GetAllAsync(AllEventsQueryModel queryModel, string userId)
    {
        var filteredEvents = await _eventService.GetAllAsync(queryModel, userId);

        queryModel.EntityViewModels = filteredEvents.Events;
        queryModel.TotalEntitiesCount = filteredEvents.TotalEventsCount;

        return queryModel;
    }

    protected override async Task<IEnumerable<EventViewModel>> GetAllEntitiesByUserId(string userId)
    {
        return await _eventService.AllEventsByUserIdAsync(userId);
    }

    protected override async Task<IEnumerable<EventViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        return await _eventService.GetEventsByPublisherIdAsync(publisherId);
    }

    protected override async Task<EventDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        return await _eventService.GetEventDetailsAsync(id, userId);
    }

    protected override async Task<EventFormModel> GetEntityInfoAsync(string id)
    {
        return await _eventService.GetEventInfoAsync(id);
    }

    protected override EventFormModel CreateFormModelInstance()
    {
        return new EventFormModel()
        {
            StartDateTime = DateTime.Now.Date,
            EndDateTime = DateTime.Now.Date
        };
    }

    protected override async Task ValidateModelAsync(EventFormModel formModel, bool isUserAdmin)
    {
        if (formModel.Price < 0)
        {
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeZeroOrHigherErrorMessage);
        }

        if (formModel.StartDateTime < DateTime.Now)
        {
            ModelState.AddModelError(nameof(formModel.StartDateTime), string.Format(WrongDateErrorMessage, "Start date", "today's date"));
        }

        if (formModel.StartDateTime > formModel.EndDateTime)
        {
            ModelState.AddModelError(nameof(formModel.EndDateTime), string.Format(WrongDateErrorMessage, "End date", "start date"));
        }

        if (!formModel.IsOnline &&
            (string.IsNullOrEmpty(formModel.LocationName) || string.IsNullOrEmpty(formModel.LocationUrl)))
        {
            ModelState.AddModelError(nameof(formModel.IsOnline), string.Format(SpecifyParticipationErrorMessage));
            ModelState.AddModelError(nameof(formModel.LocationName), string.Format(SpecifyParticipationErrorMessage));
            ModelState.AddModelError(nameof(formModel.LocationUrl), string.Format(SpecifyParticipationErrorMessage));
        }

        if (!ModelState.IsValid)
        {
            return;
        }

        await base.ValidateModelAsync(formModel, isUserAdmin);
    }
}