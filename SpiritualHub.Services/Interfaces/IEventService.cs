namespace SpiritualHub.Services.Interfaces;

using Models.Event;
using Client.ViewModels.Event;

public interface IEventService
{
    Task<int>                           GetAllCountAsync();

    Task<EventDetailsViewModel>         GetEventDetailsAsync(string id, string userId);

    Task<EventFormModel>                GetEventInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredEventsServiceModel>    GetAllAsync(AllEventsQueryModel queryModel, string userId);

    Task<IEnumerable<EventViewModel>>   AllEventsByUserIdAsync(string userId);

    Task<IEnumerable<EventViewModel>>   GetEventsByPublisherIdAsync(string publisherId);

    Task<string>                        GetAuthorIdAsync(string eventId);

    Task<string>                        CreateAsync(EventFormModel newEvent);

    Task                                EditAsync(EventFormModel updatedEvent);

    Task                                DeleteAsync(string eventId);

    Task<bool>                          IsJoinedAsync(string eventId, string userId);

    Task                                JoinAsync(string eventId, string userId);

    Task                                LeaveAsync(string eventId, string userId);
}
