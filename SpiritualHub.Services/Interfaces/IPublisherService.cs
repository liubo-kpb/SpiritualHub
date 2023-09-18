namespace SpiritualHub.Services.Interfaces;

using Data.Models;
using Client.ViewModels.Author;
using Client.ViewModels.Publisher;

public interface IPublisherService
{
    Task<bool>                                  ExistsByUserIdAsync(string Id);

    Task<bool>                                  ExistsByIdAsync(string Id);

    Task<bool>                                  UserWithPhoneNumberExists(string phoneNumber);

    Task<bool>                                  UserHasSubscriptions(string userId);

    Task                                        Create(string userId, string phoneNumber);

    Task<Publisher?>                            GetPublisherAsync(string userId);

    Task<bool>                                  IsConnectedToEntityByUserId<TEntityType>(string userId, string entityId);

    Task<bool>                                  IsConnectedToEntityByPublisherId<TEntityType>(string publisherId, string entityId);

    Task<IEnumerable<AuthorInfoViewModel>>      GetConnectedAuthorsAsync(string userId);

    Task<IEnumerable<PublisherInfoViewModel>>   GetAllAsync();

    Task<string>                                GetPublisherIdAsync(string userId);
}
