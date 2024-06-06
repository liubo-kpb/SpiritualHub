namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using AutoMapper;

using Interfaces;
using Mappings;
using Models.Event;
using Data.Models;
using Data.Repository.Interfaces;
using Client.Infrastructure.Enums;
using Client.ViewModels.Event;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IDeletableRepository<Image> _imageRepository;
    private readonly IDeletableRepository<Rating> _ratingRepository;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public EventService(
        IEventRepository eventRepository,
        IDeletableRepository<Image> imageRepository,
        IDeletableRepository<Rating> ratingRepository,
        IRepository<ApplicationUser> userRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _imageRepository = imageRepository;
        _ratingRepository = ratingRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<FilteredEventsServiceModel> GetAllAsync(AllEventsQueryModel queryModel, string userId)
    {
        IQueryable<Event> eventsQuery = _eventRepository
                            .GetAll()
                            .Where(e => e.StartDateTime > DateTime.Now);

        int eventCount = eventsQuery.Count();

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            eventsQuery = eventsQuery.Where(e => e.Category != null && e.Category.Name.ToLower().Contains(queryModel.CategoryName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = queryModel.SearchTerm.ToLower();

            eventsQuery = eventsQuery.Where(e => e.Title.ToLower().Contains(wildCard)
                                              || e.Description.ToLower().Contains(wildCard)
                                              || (e.LocationName != null && e.LocationName.ToLower().Contains(wildCard))
                                              || e.Author.Name.ToLower().Contains(wildCard));
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
                                        .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
                                        .Take(queryModel.EntitiesPerPage)
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
            TotalEventsCount = eventCount,
        };
    }

    public async Task<EventDetailsViewModel> GetEventDetailsAsync(string id, string userId)
    {
        var eventEntity = await _eventRepository.GetFullEventDetailsAsync(id);
        var eventModel = _mapper.Map<EventDetailsViewModel>(eventEntity);

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
        var eventEntity = await _eventRepository.GetEventInfoAsync(id);
        var eventModel = _mapper.Map<EventFormModel>(eventEntity);

        return eventModel;
    }

    public async Task EditAsync(EventFormModel updatedEvent)
    {
        var eventEntity = await _eventRepository.GetEventInfoAsync(updatedEvent.Id!);

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
        eventEntity!.AuthorID = Guid.Parse(updatedEvent.AuthorId!);
        eventEntity!.PublisherID = Guid.Parse(updatedEvent.PublisherId!);

        await _eventRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(string eventId)
    {
        var eventEntity = await _eventRepository.GetEventWithImageAndRatingsAsync(eventId);

        _eventRepository.Delete(eventEntity!);
        _imageRepository.Delete(eventEntity!.Image);
        _ratingRepository.DeleteMultiple(eventEntity!.Ratings);

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
        }

        return eventsModel;
    }

    public async Task<IEnumerable<EventViewModel>> GetEventsByPublisherIdAsync(string publisherId)
    {
        var events = await _eventRepository
                                .AllAsNoTracking()
                                .Include(e => e.Author)
                                .Include(e => e.Image)
                                // .Include(e => e.Participants) // still wondering if I should list all the signed up users for the event.
                                .Where(e => e.PublisherID.ToString() == publisherId)
                                .ToListAsync();

        var eventsModel = new List<EventViewModel>();
        _mapper.MapListToViewModel(events, eventsModel);

        return eventsModel;
    }

    public async Task<string> GetAuthorIdAsync(string eventId) => (await _eventRepository.GetAuthorIdAsync(eventId))!;

    public async Task<bool> IsJoinedAsync(string eventId, string userId)
    {
        var @event = await _eventRepository.GetEventWithParticipantsAsync(eventId);

        if (@event!.Participants.Any(p => p.Id.ToString() == userId))
        {
            return true;
        }

        return false;
    }
    public async Task JoinAsync(string eventId, string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(userId);
        var eventEntity = await _eventRepository.GetSingleByIdAsync(eventId);

        eventEntity!.Participants.Add(user!);
        await _eventRepository.SaveChangesAsync();
    }

    public async Task LeaveAsync(string eventId, string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(userId);
        var eventEntity = await _eventRepository.GetEventWithParticipantsAsync(eventId);

        eventEntity!.Participants.Remove(user!);
        await _eventRepository.SaveChangesAsync();
    }

    private void SetIsUserJoined(string userId, Event eventEntity, EventViewModel eventModel)
    {
        eventModel.IsUserJoined = eventEntity.Participants.Any(p => p.Id.ToString() == userId);
    }
}
