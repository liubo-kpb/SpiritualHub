namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Interfaces;
using Client.ViewModels.Author;
using Data.Repository.Interface;
using Data.Models;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository,
                         IMapper mapper)
    {
        _authorRepository = repository;
        _mapper = mapper;
    }

    public async Task<string> CreateAuthor(AuthorFormModel newAuthor, Publisher publisher)
    {
        var avatarImage = new Image
        {
            Name = newAuthor.Name,
            URL = newAuthor.AvatarImageUrl,
        };

        var author = _mapper.Map<Author>(newAuthor);
        author.AvatarImage = avatarImage;
        author.Publishers.Add(publisher);

        await _authorRepository.AddAsync(author);

        await _authorRepository.SaveChangesAsync();

        return author.Id.ToString();
    }

    public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
    {
        IEnumerable<Author> authors = await _authorRepository.AllAsNoTrackingAsync();
        ICollection<AuthorViewModel> allAuthorsModel = new List<AuthorViewModel>();
        MapListToViewModel<AuthorViewModel>(authors, allAuthorsModel);

        return allAuthorsModel;
    }

    public async Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors()
    {
        IEnumerable<Author> authors = await _authorRepository.LastThreeAuthors();
        ICollection<AuthorIndexViewModel> allAuthorsModel = new List<AuthorIndexViewModel>();
        MapListToViewModel<AuthorIndexViewModel>(authors, allAuthorsModel);

        return allAuthorsModel;
    }
 
    private void MapListToViewModel<T>(IEnumerable<Author> authors, ICollection<T> allAuthorsModel)
    {
        foreach (var author in authors)
        {
            T authorViewModel = _mapper.Map<T>(author);
            allAuthorsModel.Add(authorViewModel);
        }
    }
}
