namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Interfaces;
using Client.ViewModels.Author;
using Data.Repository.Interface;
using Data.Models;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _repository;

    public AuthorService(IRepository<Author> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AllAuthorsQueryModel>> GetAllAuthorsAsync()
    {
        IEnumerable<Author> authors = await _repository.AllAsNoTrackingAsync();
        IEnumerable<AllAuthorsQueryModel> model = new List<AllAuthorsQueryModel>();



        return model;
    }
}
