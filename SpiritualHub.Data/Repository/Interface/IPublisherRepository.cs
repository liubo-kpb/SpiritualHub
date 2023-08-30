namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface IPublisherRepository : IRepository<Publisher>
{
    Task<bool> IsConnectedPublisherAsync<TEntityType>(string userId, string entityId);

    Task<IEnumerable<Author>> GetConnectedAuthorsAsync(string userId);
}
