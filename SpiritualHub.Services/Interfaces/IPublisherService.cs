namespace SpiritualHub.Services.Interfaces;

using Data.Models;
using Client.ViewModels.Author;
using Client.ViewModels.Publisher;

public interface IPublisherService
{
    Task<bool>                                  ExistsByUserId(string Id);

    Task<bool>                                  ExistsById(string Id);

    Task<bool>                                  UserWithPhoneNumberExists(string phoneNumber);

    Task<bool>                                  UserHasSubscriptions(string userId);

    Task                                        Create(string userId, string phoneNumber);

    Task<Publisher?>                            GetPublisherAsync(string userId);

    Task<bool>                                  IsConnectedToEntity<TEntityType>(string userId, string entityId);

    Task<IEnumerable<AuthorInfoViewModel>>      GetConnectedAuthorsAsync(string userId);

    Task<IEnumerable<PublisherInfoViewModel>>   GetAllAsync();

    Task<string>                                GetPublisherIdAsync(string userId);
}
