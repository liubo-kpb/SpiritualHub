namespace SpiritualHub.Data.Repository.Interface;

using SpiritualHub.Data.Models;

public interface IBookRepository : IDeletableRepository<Book>
{
    Task<Book?> GetFullBookDetailsAsync(string id);

    Task<Book?> GetBookInfoAsync(string id);
}
