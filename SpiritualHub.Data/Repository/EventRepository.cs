﻿namespace SpiritualHub.Data.Repository;

using Interface;
using Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EventRepository : DeletableRepository<Event>, IEventRepository
{
    public EventRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<Event?> GetEventWithAuthorAsync(string eventId) => await DbSet
                                                                        .Include(e => e.Author)
                                                                        .FirstOrDefaultAsync(e => e.Id.ToString() == eventId);

    public async Task<Event?> GetEventInfo(string id) => await DbSet
                                                                .Include(e => e.Image)
                                                                .Include(e => e.Author)
                                                                .Include(e => e.Category)
                                                                .Include(e => e.Publisher)
                                                                .FirstOrDefaultAsync(e => e.Id.ToString() == id);

    public Task<Event?> GetEventWithImageAsync(string eventId) => DbSet
                                                                    .Include(e => e.Image)
                                                                    .FirstOrDefaultAsync(e => e.Id.ToString() == eventId);

    public async Task<Event?> GetEventWithParticipantsAsync(string eventId) => await DbSet
                                                                                        .Include(e => e.Participants)
                                                                                        .FirstOrDefaultAsync(e => e.Id.ToString() == eventId);

    public async Task<Event?> GetFullEventDetails(string id) => await DbSet
                                                                        .Include(e => e.Image)
                                                                        .Include(e => e.Author)
                                                                        .Include(e => e.Category)
                                                                        .Include(e => e.Participants)
                                                                        .Include(e => e.Publisher)
                                                                        .ThenInclude(p => p.User)
                                                                        .FirstOrDefaultAsync(e => e.Id.ToString() == id);
}