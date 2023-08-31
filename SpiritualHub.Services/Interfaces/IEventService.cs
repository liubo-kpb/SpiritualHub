namespace SpiritualHub.Services.Interfaces;

using Models.Event;
using Client.ViewModels.Event;

public interface IEventService
{
    Task<int>                           GetAllCountAsync();

    Task<EventDetailsViewModel>         GetEventDetailsAsync(string id, string userId);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredEventsServiceModel>    GetAllAsync(AllEventsQueryModel queryModel, string userId);

    Task<string>                        CreateEventAsync(EventFormModel newEvent, string publisherId);
}
