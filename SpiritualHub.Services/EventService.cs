﻿namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using AutoMapper;

using Interfaces;
using Mappings;
using Models.Event;
using Data.Models;
using Data.Repository.Interface;
using Client.Infrastructure.Enums;
using Client.ViewModels.Event;

public class EventService : IEventService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public EventService(
        IRepository<Event> eventRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<FilteredEventsServiceModel> GetAllAsync(AllEventsQueryModel queryModel, string userId)
    {
        IQueryable<Event> eventsQuery = _eventRepository
                            .GetAll()
                            .Where(e => e.StartDateTime > DateTime.Now);

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            eventsQuery = eventsQuery.Where(a => a.Category.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

            eventsQuery = eventsQuery.Where(a => EF.Functions.Like(a.Title, wildCard)
                                      || EF.Functions.Like(a.Description, wildCard)
                                      || EF.Functions.Like(a.LocationName, wildCard)
                                      || EF.Functions.Like(a.Author.Name, wildCard));
        }

        eventsQuery = queryModel.SortingOption switch
        {
            EventSorting.Newest => eventsQuery.OrderByDescending(e => e.CreatedOn),
            EventSorting.Oldest => eventsQuery.OrderBy(e => e.CreatedOn),
            EventSorting.ParticipantsAscending => eventsQuery.OrderBy(e => e.Participants.Count),
            EventSorting.ParticipantsDescending => eventsQuery.OrderByDescending(e => e.Participants.Count),
            EventSorting.PriceAscending => eventsQuery.OrderBy(e => e.Price),
            EventSorting.PriceDescending => eventsQuery.OrderByDescending(e => e.Price),
            EventSorting.Soonest => eventsQuery.OrderBy(e => e.StartDateTime),
            EventSorting.Latest => eventsQuery.OrderBy(e => e.EndDateTime),
            EventSorting.TopRated => eventsQuery.OrderByDescending(e => (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            EventSorting.LeastRated => eventsQuery.OrderBy(e => (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            _ => eventsQuery
                    .OrderBy(e => e.StartDateTime)
                    .ThenByDescending(e => (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0)))
                    .ThenBy(e => e.Price)
        };

        List<Event> events = await eventsQuery
                                        .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                                        .Take(queryModel.EventsPerPage)
                                        .Include(e => e.Image)
                                        .Include(e => e.Author)
                                        .Include(e => e.Participants)
                                        .ToListAsync();

        List<EventViewModel> eventsModel = new List<EventViewModel>();
        _mapper.MapListToViewModel(events, eventsModel);

        for (int i = 0; i < events.Count; i++)
        {
            SetIsUserJoined(userId, events[i], eventsModel[i]);
        }

        return new FilteredEventsServiceModel()
        {
            Events = eventsModel,
            TotalEventsCount = eventsQuery.Count(),
        };
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _eventRepository
            .AllAsNoTracking()
            .CountAsync();
    }

    private void SetIsUserJoined(string userId, Event eventEntity, EventViewModel eventModel)
    {
        eventModel.IsUserJoined = eventEntity.Participants.Any(p => p.Id.ToString() == userId);
    }
}
