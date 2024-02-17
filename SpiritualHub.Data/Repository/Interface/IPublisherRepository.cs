namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface IPublisherRepository : IRepository<Publisher>
{
    Task<IEnumerable<Author>>       GetConnectedAuthorsAsync(string userId);

    Task<IEnumerable<Publisher>>    GetAllPublishersInfoAsync();

    Task<string?>                   GetPublisherId(string userId);
}
