namespace SpiritualHub.Data.Repository;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

using Data.Models;
using Repository.Interface;

public class AuthorRepository : DeletableRepository<Author>, IAuthorRepository
{
    public AuthorRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Author>> LastThreeAuthors()
    {
        return await DbSet
                        .Include(a => a.AvatarImage)
                        .Where(a => a.IsActive)
                        .OrderByDescending(a => a.AddedOn)
                        .Take(3)
                        .ToArrayAsync();
    }

    public async Task<Author> GetAuthorDetailsByIdAsync(string id)
    {
        return await DbSet
                        .Include(a => a.AvatarImage)
                        .Include(a => a.Category)
                        .Include(a => a.Publishers)
                        .ThenInclude(p => p.User)
                        .Include(a => a.Followers)
                        .Include(a => a.Subscriptions)
                        .ThenInclude(s => s.Subscribers)
                        .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }

    public async Task<Author> GetAuthorByIdWithAvatar(string id)
    {
        return await DbSet
            .Include(a => a.AvatarImage)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }

    public async Task<Author> GetAuthorWithPublishersAsync(string id)
    {
        return await DbSet
            .Include(a => a.Publishers)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }

    public async Task<Author> GetAuthorWithSubscriptionsAndSubscribersAsync(string id)
    {
        return await DbSet
            .Include(a => a.Subscriptions)
            .ThenInclude(s => s.Subscribers)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }

    public async Task<Author> GetAuthorWithFollowersAsync(string id)
    {
        return await DbSet
            .Include(a => a.Followers)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }

    public async Task<Author> GetAuthorWithSubscriptionsAsync(string id)
    {
        return await DbSet
            .Include(a => a.AvatarImage)
            .Include(a => a.Subscriptions)
            .ThenInclude(s => s.SubscriptionType)
            .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }
}
