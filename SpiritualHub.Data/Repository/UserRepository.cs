namespace SpiritualHub.Data.Repository;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Models;
using Interface;

public class UserRepository : Repository<ApplicationUser>, IUserRepository
{
    public UserRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<ApplicationUser?> GetUserWithBooks(string id) => await DbSet
                                                                                .Include(u => u.Books)
                                                                                .ThenInclude(b => b.Image)
                                                                                .Include(u => u.Books)
                                                                                .ThenInclude(b => b.Author)
                                                                                .FirstOrDefaultAsync(u => u.Id.ToString() == id);
}
