namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Infrastructure.Enums;
using ViewModels.Event;
using Services.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

public class EventController : ProductController<EventViewModel, EventDetailsViewModel, EventFormModel, AllEventsQueryModel, EventSorting>
{
    private readonly IEventService _eventService;

    public EventController(
        IEventService eventService,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IServiceProvider serviceProvider)
        : base (serviceProvider, urlHelperFactory, actionContextAccessor, nameof(Event).ToLower())
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task<IActionResult> Join(string id)
    {
        return await Get(id);
    }

    [HttpPost]
    public async Task<IActionResult> Leave(string id)
    {
        return await Remove(id);
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

    protected override async Task<IEnumerable<EventViewModel>> GetAllEntitiesByUserIdAsync(string userId)
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

    protected override async Task GetAsync(string id, string userId)
    {
        await _eventService.JoinAsync(id, userId);
    }

    protected override async Task RemoveAsync(string id, string userId)
    {
        await _eventService.LeaveAsync(id, userId);
    }

    protected override async Task DeleteAsync(string id)
    {
        await _eventService.DeleteAsync(id);
    }

    protected override Task ShowAsync(string id)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override Task HideAsync(string id)
    {
        throw new NotImplementedException(InvalidRequestErrorMessage);
    }

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        return await _eventService.GetAuthorIdAsync(entityId);
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        return await _eventService.IsJoinedAsync(id, usedId);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        return AlreadyJoinedErrorMessage;
    }

    protected override string GetEntitySuccessMessage()
    {
        return JoinEventSuccessfulMessage;
    }

    protected override string RemoveEntitySuccessMessage()
    {
        return LeaveEventSuccessfulMessage;
    }

    protected override EventFormModel CreateFormModelInstance()
    {
        return new EventFormModel()
        {
            StartDateTime = DateTime.Now.Date,
            EndDateTime = DateTime.Now.Date
        };
    }

    protected override async Task ValidateModelAsync(EventFormModel formModel)
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

        await base.ValidateModelAsync(formModel);
    }

    protected override string GetAction()
    {
        return $"join {_entityName}";
    }

    protected override string RemoveAction()
    {
        return $"leave {_entityName}";
    }
}