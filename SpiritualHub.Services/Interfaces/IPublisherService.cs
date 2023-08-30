namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;
using Data.Models;

public interface IPublisherService
{
    Task<bool> ExistsById(string Id);

    Task<bool> UserWithPhoneNumberExists(string phoneNumber);

    Task<bool> UserHasSubscriptions(string userId);

    Task Create(string userId, string phoneNumber);

    Task<Publisher?> GetPublisherAsync(string userId);

    Task<bool> IsConnectedToEntity<TEntityType>(string userId, string entityId);

    Task<IEnumerable<AuthorInfoViewModel>> GetConnectedAuthorsAsync(string userId);
}
