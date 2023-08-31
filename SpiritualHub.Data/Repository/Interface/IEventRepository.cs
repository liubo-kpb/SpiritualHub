namespace SpiritualHub.Data.Repository.Interface;

using Data.Models;

public interface IEventRepository : IDeletableRepository<Event>
{
    public Task<Event?> GetFullEventDetails(string id);

    public Task<Event?> GetEventInfo(string id);
}
