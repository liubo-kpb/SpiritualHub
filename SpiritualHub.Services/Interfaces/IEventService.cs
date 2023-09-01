﻿namespace SpiritualHub.Services.Interfaces;

using Models.Event;
using Client.ViewModels.Event;

public interface IEventService
{
    Task<int>                           GetAllCountAsync();

    Task<EventDetailsViewModel>         GetEventDetailsAsync(string id, string userId);

    Task<EventFormModel>                GetEventInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredEventsServiceModel>    GetAllAsync(AllEventsQueryModel queryModel, string userId);

    Task<string>                        CreateAsync(EventFormModel newEvent);

    Task                                EditAsync(EventFormModel updatedEvent);
}
