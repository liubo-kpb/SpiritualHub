namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;

public class EventService : IEventService
{
    private readonly IRepository<Event> _eventRepository;

    public EventService(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _eventRepository
            .AllAsNoTracking()
            .CountAsync();
    }
}
