namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;
using SpiritualHub.Data.Models;

public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAllAsync();

    Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors();

    Task<string> CreateAuthor(AuthorFormModel newAuthor, Publisher publisher);
}
