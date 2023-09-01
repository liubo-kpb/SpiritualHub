﻿namespace SpiritualHub.Services;

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Mappings;
using Data.Models;
using Data.Repository.Interface;
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

    public async Task<bool> ExistsById(string Id) => await _publisherRepository
                                                                .AnyAsync(p => p.Id.ToString() == Id);

    public async Task<bool> ExistsByUserId(string Id) => await _publisherRepository
                                                                .AnyAsync(u => u.UserID.ToString() == Id);

    public async Task<IEnumerable<PublisherInfoViewModel>> GetAllAsync()
    {
        var publishers = await _publisherRepository.GetAllPublishersInfoAsync();
        var publishersModel = new List<PublisherInfoViewModel>();

        _mapper.MapListToViewModel(publishers, publishersModel);

        return publishersModel;
    }

    public async Task<IEnumerable<AuthorInfoViewModel>> GetConnectedAuthorsAsync(string userId)
    {
        var authors = await _publisherRepository.GetConnectedAuthorsAsync(userId);
        var authorsModel = new List<AuthorInfoViewModel>();
        _mapper.MapListToViewModel(authors, authorsModel);

        return authorsModel;
    }

    public async Task<Publisher?> GetPublisherAsync(string userId) => await _publisherRepository
                                                                                .GetAll()
                                                                                .FirstOrDefaultAsync(a => a.UserID.ToString() == userId);

    public async Task<string> GetPublisherIdAsync(string userId) => (await GetPublisherAsync(userId))!
                                                                            .Id
                                                                            .ToString();

    public async Task<bool> IsConnectedToEntity<TEntityType>(string userId, string entityId) => await _publisherRepository
                                                                                                            .IsConnectedPublisherAsync<TEntityType>(userId, entityId);

    public async Task<bool> UserHasSubscriptions(string userId) => await _subscriptionRepository
                                                                                .AnyAsync(s => s.Subscribers
                                                                                                    .Any(u => u.Id.ToString() == userId));

    public async Task<bool> UserWithPhoneNumberExists(string phoneNumber) => await _publisherRepository
                                                                                        .AnyAsync(u => u.PhoneNumber == phoneNumber);
}
