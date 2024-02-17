﻿namespace SpiritualHub.Data.Repository;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Models;
using Interface;

public class PublisherRepository : Repository<Publisher>, IPublisherRepository
{
    public PublisherRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersInfoAsync() => await DbSet
                                                                                    .Include(p => p.User)
                                                                                    .ToListAsync();

    public async Task<IEnumerable<Author>> GetConnectedAuthorsAsync(string userId) => (await DbSet.Include(p => p.Authors)
                                                                                                  .FirstOrDefaultAsync(p => p.UserID.ToString() == userId))!
                                                                                                  .Authors;

    public async Task<string?> GetPublisherId(string userId) => await DbSet
                                                                .Where(p => p.UserID.ToString() == userId)
                                                                .Select(p => p.Id.ToString())
                                                                .FirstOrDefaultAsync();

}
