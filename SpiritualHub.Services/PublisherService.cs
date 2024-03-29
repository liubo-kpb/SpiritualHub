namespace SpiritualHub.Services;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Mappings;
using Data.Models;
using Data.Repository.Interfaces;
using Client.ViewModels.Author;
using Client.ViewModels.Publisher;

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IRepository<Subscription> _subscriptionRepository;
    private readonly IMapper _mapper;

    public PublisherService(
        IPublisherRepository publisherRepository,
        IRepository<Subscription> subscriptionRepository,
        IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _subscriptionRepository = subscriptionRepository;
        _mapper = mapper;
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

    public async Task<bool> ExistsByIdAsync(string Id) => await _publisherRepository
                                                                .AnyAsync(p => p.Id.ToString() == Id);

    public async Task<bool> ExistsByUserIdAsync(string Id) => await _publisherRepository
                                                                .AnyAsync(u => u.UserID.ToString() == Id);

    public async Task<IEnumerable<PublisherInfoViewModel>> GetAllAsync()
    {
        var publishers = await _publisherRepository.GetAllPublishersInfoAsync();
        var publishersModel = new List<PublisherInfoViewModel>();

        _mapper.MapListToViewModel(publishers, publishersModel);

        return publishersModel;
    }

    public async Task<IEnumerable<AuthorInfoViewModel>> GetConnectedAuthorsByUserIdAsync(string userId)
    {
        var authors = await _publisherRepository.GetConnectedAuthorsAsync(userId);
        var authorsModel = new List<AuthorInfoViewModel>();
        _mapper.MapListToViewModel(authors, authorsModel);

        return authorsModel;
    }

    public async Task<Publisher?> GetPublisherAsync(string userId) => await _publisherRepository
                                                                                .GetAll()
                                                                                .FirstOrDefaultAsync(a => a.UserID.ToString() == userId);

    public async Task<string?> GetPublisherIdAsync(string userId) => await _publisherRepository
                                                                                .GetPublisherId(userId);

    public async Task<bool> IsConnectedToAuthorByPublisherId(string publisherId, string authorId) => await _publisherRepository
                                                                                                                            .AnyAsync(p => p.Id.ToString() == publisherId && p.Authors.Any(a => a.Id.ToString() == authorId));

    public async Task<bool> IsConnectedToAuthorByUserId(string userId, string authorId) => await _publisherRepository
                                                                                                                .AnyAsync(p => p.UserID.ToString() == userId && p.Authors.Any(a => a.Id.ToString() == authorId));

    public async Task<bool> UserHasSubscriptions(string userId) => await _subscriptionRepository
                                                                                .AnyAsync(s => s.Subscribers
                                                                                                    .Any(u => u.Id.ToString() == userId));

    public async Task<bool> UserWithPhoneNumberExists(string phoneNumber) => await _publisherRepository
                                                                                        .AnyAsync(u => u.PhoneNumber == phoneNumber);
}
