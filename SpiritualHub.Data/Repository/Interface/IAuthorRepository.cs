namespace SpiritualHub.Data.Repository.Interface;

using SpiritualHub.Data.Models;

public interface IAuthorRepository : IDeletableRepository<Author>
{
    Task<IEnumerable<Author>> LastThreeAuthors();

    public Task<Author> GetAuthorDetailsByIdAsync(string id);
}
