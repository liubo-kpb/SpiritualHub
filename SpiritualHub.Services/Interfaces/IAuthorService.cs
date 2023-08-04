namespace SpiritualHub.Services.Interfaces;

using SpiritualHub.Client.ViewModels.Author;

public interface IAuthorService
{
    Task<IEnumerable<AllAuthorsQueryModel>> GetAllAuthorsAsync();
}
