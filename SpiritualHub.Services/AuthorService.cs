namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Interfaces;
using Client.ViewModels.Author;
using Data.Repository.Interface;
using Data.Models;
using AutoMapper;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public AuthorService(IRepository<Author> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
    {
        IEnumerable<Author> authors = await _repository.AllAsNoTrackingAsync();
        ICollection<AuthorViewModel> allAuthorsModel = new List<AuthorViewModel>();

        foreach (var author in authors)
        {
            AuthorViewModel authorViewModel = _mapper.Map<AuthorViewModel>(author);
            allAuthorsModel.Add(authorViewModel);
        }

        return allAuthorsModel;
    }
}
