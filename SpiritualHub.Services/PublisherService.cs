namespace SpiritualHub.Services;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IRepository<Subscription> _subscriptionRepository;

    public PublisherService(
        IPublisherRepository publisherRepository,
        IRepository<Subscription> subscriptionRepository)
    {
        _publisherRepository = publisherRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task Create(string userId, string phoneNumber)
    {
        var publisher = new Publisher()
        {
            UserID = Guid.Parse(userId),
            PhoneNumber = phoneNumber
        };

        await _publisherRepository.AddAsync(publisher);
        await _publisherRepository.SaveChangesAsync();
    }

    public async Task<bool> ExistsById(string Id)
    {
        return await _publisherRepository.AnyAsync(u => u.UserID.ToString() == Id);
    }

    public async Task<Publisher> GetPublisherAsync(string userId)
    {
        return await _publisherRepository
            .AllAsNoTracking()
            .FirstOrDefaultAsync(a => a.UserID.ToString() == userId);
    }

    public async Task<bool> IsConnectedToEntity<TEntityType>(string userId, string entityId)
    {
        return await _publisherRepository
            .IsConnectedPublisherAsync<TEntityType>(userId, entityId);
    }

    public async Task<bool> UserHasSubscriptions(string userId)
    {
        return await _subscriptionRepository.AnyAsync(s => s.Subscribers.Any(u => u.Id.ToString() == userId));
    }

    public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
    {
        return await _publisherRepository.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }
}
