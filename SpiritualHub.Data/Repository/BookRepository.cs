namespace SpiritualHub.Data.Repository;

using Microsoft.EntityFrameworkCore;

using Models;
using Interface;

public class BookRepository : DeletableRepository<Book>, IBookRepository
{
    public BookRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<Book?> GetFullBookDetails(string id) => await DbSet
                                                                        .Include(b => b.Image)
                                                                        .Include(b => b.Author)
                                                                        .Include(b => b.Category)
                                                                        .Include(b => b.Publisher)
                                                                        .ThenInclude(p => p.User)
                                                                        .FirstOrDefaultAsync(b => b.Id.ToString() == id);
}
