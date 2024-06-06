namespace SpiritualHub.Data.Repository.Interfaces;

using Data.Models;
using Microsoft.Extensions.Logging;

public interface IEventRepository : IDeletableRepository<Event>
{
    Task<Event?> GetFullEventDetailsAsync(string id);

    Task<Event?> GetEventInfoAsync(string id);

    Task<Event?> GetEventWithAuthorAsync(string eventId);

    Task<Event?> GetEventWithParticipantsAsync(string eventId);

    Task<Event?> GetEventWithImageAndRatingsAsync(string eventId);

    Task<string?> GetAuthorIdAsync(string id);
}
