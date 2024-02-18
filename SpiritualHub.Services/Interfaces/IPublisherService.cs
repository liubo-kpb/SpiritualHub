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

    Task<bool>                                  IsConnectedToAuthorByUserId(string userId, string authorId);

    Task<bool>                                  IsConnectedToAuthorByPublisherId(string publisherId, string authorId);

    Task<IEnumerable<AuthorInfoViewModel>>      GetConnectedAuthorsByUserIdAsync(string userId);

    Task<IEnumerable<PublisherInfoViewModel>>   GetAllAsync();

    Task<string?>                                GetPublisherIdAsync(string userId);
}
