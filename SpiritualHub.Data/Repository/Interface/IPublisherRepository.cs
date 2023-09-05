namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface IPublisherRepository : IRepository<Publisher>
{
    Task<bool>                      IsConnectedPublisherByUserIdAsync<TEntityType>(string userId, string entityId);

    Task<bool>                      IsConnectedPublisherByPublisherIdAsync<TEntityType>(string publisherId, string entityId);

    Task<IEnumerable<Author>>       GetConnectedAuthorsAsync(string userId);

    Task<IEnumerable<Publisher>>    GetAllPublishersInfoAsync();
}
