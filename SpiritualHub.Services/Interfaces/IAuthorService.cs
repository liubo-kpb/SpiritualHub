namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;

public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync();

    Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors();
}
