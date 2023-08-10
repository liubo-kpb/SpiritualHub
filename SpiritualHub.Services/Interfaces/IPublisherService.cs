namespace SpiritualHub.Services.Interfaces;

using SpiritualHub.Data.Models;

public interface IPublisherService
{
    Task<bool> ExistsById(string Id);

    Task<bool> UserWithPhoneNumberExists(string phoneNumber);

    Task<bool> UserHasSubscriptions(string userId);

    Task Create(string userId, string phoneNumber);

    Task<Publisher> GetPublisher(string userId);
}
