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
                        .OrderBy(a => a.AddedOn)
                        .Take(3)
                        .ToArrayAsync();
    }
}
