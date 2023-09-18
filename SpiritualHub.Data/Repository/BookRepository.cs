namespace SpiritualHub.Data.Repository;

using Microsoft.EntityFrameworkCore;

using Models;
using Interface;

public class BookRepository : DeletableRepository<Book>, IBookRepository
{
    public BookRepository(SpiritsDbContext context) : base(context)
    {
    }

    public async Task<Book?> GetBookInfoAsync(string id) => await DbSet
                                                                    .Include(b => b.Image)
                                                                    .Include(b => b.Author)
                                                                    .Include(b => b.Category)
                                                                    .Include(b => b.Publisher)
                                                                    .FirstOrDefaultAsync(b => b.Id.ToString() == id);

    public async Task<Book?> GetBookWithReaders(string id) => await DbSet
                                                                        .Include(b => b.Readers)
                                                                        .FirstOrDefaultAsync(b => b.Id.ToString() == id);

    public async Task<Book?> GetFullBookDetailsAsync(string id) => await DbSet
                                                                        .Include(b => b.Image)
                                                                        .Include(b => b.Author)
                                                                        .Include(b => b.Category)
                                                                        .Include(b => b.Publisher)
                                                                        .ThenInclude(p => p.User)
                                                                        .Include(b => b.Readers)
                                                                        .FirstOrDefaultAsync(b => b.Id.ToString() == id);
}
