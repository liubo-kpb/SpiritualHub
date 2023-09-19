namespace SpiritualHub.Data.Repository.Interface;

using Data.Models;
using Microsoft.Extensions.Logging;

public interface IEventRepository : IDeletableRepository<Event>
{
    Task<Event?> GetFullEventDetails(string id);

    Task<Event?> GetEventInfo(string id);

    Task<Event?> GetEventWithAuthorAsync(string eventId);

    Task<Event?> GetEventWithParticipantsAsync(string eventId);

    Task<Event?> GetEventWithImageAsync(string eventId);
}
