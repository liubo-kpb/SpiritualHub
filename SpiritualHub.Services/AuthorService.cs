namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Models.Author;
using Data.Repository.Interface;
using Data.Models;
using Client.ViewModels.Author;
using Client.Infrastructure.Enums;

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

    public async Task<IEnumerable<AuthorViewModel>> AllAuthorsByPublisherId(string publisherId)
    {
        var authors = await _authorRepository
            .GetAll()
            .Where(a => a.Publishers.Any(p => p.Id.ToString() == publisherId))
            .ToListAsync();

        ICollection<AuthorViewModel> authorsModel = new HashSet<AuthorViewModel>();
        MapListToViewModel<AuthorViewModel>(authors, authorsModel);

        return authorsModel;
    }

    public async Task<IEnumerable<AuthorViewModel>> AllAuthorsByUserId(string userId)
    {
        var authors = await _authorRepository
            .GetAll()
            .Where(a => a.Followers.Any(u => u.Id.ToString() == userId) 
                    || a.Subscriptions.Any(s => s.Subscribers.Any(ss => ss.Id.ToString() == userId)))
            .ToListAsync();

        ICollection<AuthorViewModel> authorsModel = new HashSet<AuthorViewModel>();
        MapListToViewModel<AuthorViewModel>(authors, authorsModel);

        return authorsModel;
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

    public async Task<FilteredAuthorsServiceModel> GetAllAsync(AllAuthorsQueryModel queryModel)
    {
        IQueryable<Author> authorsQuery = _authorRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            authorsQuery = authorsQuery.Where(a => a.Category.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

            authorsQuery = authorsQuery.Where(a => EF.Functions.Like(a.Alias, wildCard)
                                      || EF.Functions.Like(a.Name, wildCard));
        }

        authorsQuery = queryModel.SortingOption switch
        {
            AuthorSorting.Newest => authorsQuery.OrderByDescending(a => a.AddedOn),
            AuthorSorting.Oldest => authorsQuery.OrderBy(a => a.AddedOn),
            AuthorSorting.FollowersDescending => authorsQuery.OrderByDescending(a => a.Followers.Count),
            AuthorSorting.FollowersAscending => authorsQuery.OrderBy(a => a.Followers.Count),
            AuthorSorting.SubscribersDescending => authorsQuery.OrderByDescending(a => a.Subscriptions.Count),
            AuthorSorting.SubscribersAscending => authorsQuery.OrderBy(a => a.Subscriptions.Count),
            _ => authorsQuery.OrderByDescending(a => a.Followers.Count)
                                                               .ThenByDescending(a => a.AddedOn)
        };

        IEnumerable<Author> authors = await authorsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.AuthorsPerPage)
            .Take(queryModel.AuthorsPerPage)
            .Include(a => a.AvatarImage)
            .Include(a => a.Followers)
            .Include(a => a.Subscriptions)
            .ThenInclude(s => s.Subscribers)
            .ToArrayAsync();

        ICollection<AuthorViewModel> allAuthorsModel = new List<AuthorViewModel>();
        MapListToViewModel<AuthorViewModel>(authors, allAuthorsModel);

        return new FilteredAuthorsServiceModel()
        {
            Authors = allAuthorsModel,
            TotalAuthorsCount = allAuthorsModel.Count,
        };
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
