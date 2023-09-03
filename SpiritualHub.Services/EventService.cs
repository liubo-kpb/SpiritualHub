namespace SpiritualHub.Services;

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
    private readonly IEventRepository _eventRepository;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public EventService(
        IEventRepository eventRepository,
        IRepository<ApplicationUser> userRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _userRepository = userRepository;
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
            EventSorting.TopRated => eventsQuery.OrderByDescending(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            EventSorting.LeastRated => eventsQuery.OrderBy(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            _ => eventsQuery
                    .OrderBy(e => e.StartDateTime)
                    .ThenByDescending(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0)))
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
            SetEventParticipationType(eventsModel[i]);
        }

        return new FilteredEventsServiceModel()
        {
            Events = eventsModel,
            TotalEventsCount = eventsQuery.Count(),
        };
    }

    public async Task<EventDetailsViewModel> GetEventDetailsAsync(string id, string userId)
    {
        var eventEntity = await _eventRepository.GetFullEventDetails(id);
        var eventModel = _mapper.Map<EventDetailsViewModel>(eventEntity);

        SetEventParticipationType(eventModel);
        SetIsUserJoined(userId, eventEntity!, eventModel);

        return eventModel;
    }

    public async Task<int> GetAllCountAsync() => await _eventRepository
                                                            .AllAsNoTracking()
                                                            .CountAsync();

    public async Task<bool> ExistsAsync(string id)
        => await _eventRepository.AnyAsync(e => e.Id.ToString() == id);

    public async Task<string> CreateAsync(EventFormModel newEvent)
    {
        var newEventEntity = _mapper.Map<Event>(newEvent);
        newEventEntity.Image.Name = newEvent.Title;

        await _eventRepository.AddAsync(newEventEntity);
        await _eventRepository.SaveChangesAsync();

        return newEventEntity.Id.ToString();
    }

    public async Task<EventFormModel> GetEventInfoAsync(string id)
    {
        var eventEntity = await _eventRepository.GetEventInfo(id);
        var eventModel = _mapper.Map<EventFormModel>(eventEntity);

        return eventModel;
    }

    public async Task EditAsync(EventFormModel updatedEvent)
    {
        var eventEntity = await _eventRepository.GetEventInfo(updatedEvent.Id.ToString());

        eventEntity!.Title = updatedEvent.Title;
        eventEntity!.Description = updatedEvent.Description;
        eventEntity!.Price = updatedEvent.Price;
        eventEntity!.StartDateTime = updatedEvent.StartDateTime;
        eventEntity!.EndDateTime = updatedEvent.EndDateTime;
        eventEntity!.LocationName = updatedEvent.LocationName;
        eventEntity!.LocationUrl = updatedEvent.LocationUrl;
        eventEntity!.IsOnline = updatedEvent.IsOnline;
        eventEntity!.Image.URL = updatedEvent.ImageUrl;
        eventEntity!.CategoryID = updatedEvent.CategoryId;
        eventEntity!.AuthorID = Guid.Parse(updatedEvent.AuthorId);
        eventEntity!.PublisherID = Guid.Parse(updatedEvent.PublisherId!);

        await _eventRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(string eventId)
    {
        Guid entityId = Guid.Parse(eventId);
        var eventEntity = await _eventRepository.GetSingleByIdAsync(entityId);

        _eventRepository.DeleteEntriesWithForeignKeys<Rating, Guid>($"{nameof(Event)}ID", entityId);
        _eventRepository.Delete(eventEntity);

        await _eventRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventViewModel>> AllEventsByUserIdAsync(string userId)
    {
        var events = await _eventRepository
                                .AllAsNoTracking()
                                .Include(e => e.Author)
                                .Include(e => e.Image)
                                .Include(e => e.Participants)
                                .Where(e => e.Participants.Any(u => u.Id.ToString() == userId))
                                .ToListAsync();

        var eventsModel = new List<EventViewModel>();
        _mapper.MapListToViewModel(events, eventsModel);

        for (int i = 0; i < events.Count; i++)
        {
            eventsModel[i].IsUserJoined = true;
            SetEventParticipationType(eventsModel[i]);
        }

        return eventsModel;
    }

    public async Task<IEnumerable<EventViewModel>> GetEventsByPublisherIdAsync(string publisherId)
    {
        var events = await _eventRepository
                                .AllAsNoTracking()
                                .Include(e => e.Author)
                                .Include(e => e.Image)
                                // .Include(e => e.Participants) still wondering if I should list all the signed up users for the event.
                                .Where(e => e.PublisherID.ToString() == publisherId)
                                .ToListAsync();

        var eventsModel = new List<EventViewModel>();
        _mapper.MapListToViewModel(events, eventsModel);

        for (int i = 0; i < events.Count; i++)
        {
            SetEventParticipationType(eventsModel[i]);
        }

        return eventsModel;
    }

    public async Task<string> GetAuthorIdAsync(string eventId) => (await _eventRepository.GetAuthorIdAsync(eventId))!
                                                                                                .AuthorID.ToString();

    public async Task<bool> IsJoinedAsync(string eventId, string userId) => await _eventRepository
                                                                                    .AnyAsync(
                                                                                        e => e.Participants
                                                                                                    .Any(u => u.Id.ToString() == userId));

    public async Task<bool> HasLeftAsync(string eventId, string userId) => (await _eventRepository
                                                                                    .GetEventWithParticipantsAsync(eventId))!
                                                                                    .Participants.Any(u => !(u.Id.ToString() == userId));
    public async Task JoinAsync(string eventId, string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));
        var eventEntity = await _eventRepository.GetSingleByIdAsync(Guid.Parse(eventId));

        eventEntity.Participants.Add(user);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task LeaveAsync(string eventId, string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));
        var eventEntity = await _eventRepository.GetEventWithParticipantsAsync(eventId);

        eventEntity!.Participants.Remove(user);
        await _eventRepository.SaveChangesAsync();
    }

    private void SetIsUserJoined(string userId, Event eventEntity, EventViewModel eventModel)
    {
        eventModel.IsUserJoined = eventEntity.Participants.Any(p => p.Id.ToString() == userId);
    }

    private void SetEventParticipationType(EventViewModel eventModel)
    {
        if (!string.IsNullOrEmpty(eventModel.LocationName) && eventModel.IsOnline)
        {
            eventModel.Participation = "In Person and Online";
        }
        else if (eventModel.IsOnline)
        {
            eventModel.Participation = "Online only";
        }
        else
        {
            eventModel.Participation = "In Person only";
        }
    }

}
