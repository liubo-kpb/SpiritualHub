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
                        .FirstOrDefaultAsync(a => a.Id.ToString() == id);
    }
}
