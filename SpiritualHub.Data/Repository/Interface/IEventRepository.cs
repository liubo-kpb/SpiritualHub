namespace SpiritualHub.Data.Repository.Interface;

using Data.Models;
using Microsoft.Extensions.Logging;

public interface IEventRepository : IDeletableRepository<Event>
{
    Task<Event?> GetFullEventDetails(string id);

    Task<Event?> GetEventInfo(string id);

    Task<Event?> GetAuthorIdAsync(string eventId);

    Task<Event?> GetEventWithParticipantsAsync(string eventId);
}
