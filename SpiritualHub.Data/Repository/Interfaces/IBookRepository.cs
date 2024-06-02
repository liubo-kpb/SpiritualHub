namespace SpiritualHub.Data.Repository.Interfaces;

using SpiritualHub.Data.Models;

public interface IBookRepository : IDeletableRepository<Book>
{
    Task<Book?> GetFullBookDetailsAsync(string id);

    Task<Book?> GetBookInfoAsync(string id);

    Task<Book?> GetBookWithReaders(string id);

    Task<Book?> GetBookWithImageAndRatingsAsync(string id);

    Task<Book?> GetBookWithAuthorAsync(string id);

    Task<string?> GetBookAuthorId(string id);

    Task<bool>  IsHiddenAsync(string id);
}
