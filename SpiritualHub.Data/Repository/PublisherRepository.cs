namespace SpiritualHub.Data.Repository;

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

    public async Task<bool> IsConnectedPublisherAsync<TEntityType>(string userId, string entityId)
    {
        string propertyName = typeof(TEntityType).Name + "s";

        var entity = await DbSet
            .Include(propertyName)
            .FirstOrDefaultAsync(p => p.UserID.ToString() == userId);

        var property = entity!.GetType().GetProperty(propertyName);

        var propertyValue = property!.GetValue(entity);
        if (propertyValue is IEnumerable<object> items)
        {
            return items.Any(item => item.GetType().GetProperty("Id")?.GetValue(item)?.ToString() == entityId);
        }

        throw new ArgumentException($"Property {propertyName} is not a collection.");
    }

}
