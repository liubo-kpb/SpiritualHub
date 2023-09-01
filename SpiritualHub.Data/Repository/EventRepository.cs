namespace SpiritualHub.Data.Repository;

using Interface;
using Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class EventRepository : DeletableRepository<Event>, IEventRepository
{
    public EventRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<Event?> GetEventInfo(string id) => await DbSet
                                                                .Include(e => e.Image)
                                                                .Include(e => e.Author)
                                                                .Include(e => e.Category)
                                                                .FirstOrDefaultAsync(e => e.Id.ToString() == id);

    public async Task<Event?> GetFullEventDetails(string id) => await DbSet
                                                                        .Include(e => e.Image)
                                                                        .Include(e => e.Author)
                                                                        .Include(e => e.Category)
                                                                        .Include(e => e.Participants)
                                                                        .Include(e => e.Publisher)
                                                                        .ThenInclude(p => p.User)
                                                                        .FirstOrDefaultAsync(e => e.Id.ToString() == id);
}
