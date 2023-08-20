namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Mappings;
using Models.Author;
using Data.Repository.Interface;
using Data.Models;
using Client.ViewModels.Author;
using Client.Infrastructure.Enums;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository,
                         IRepository<ApplicationUser> userRepository,
                         IMapper mapper)
    {
        _authorRepository = repository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<AuthorViewModel>> AllAuthorsByPublisherId(string userId, string publisherId)
    {
        var authors = await _authorRepository
            .GetAll()
            .Include(a => a.AvatarImage)
            .Include(a => a.Followers)
            .Where(a => a.Publishers.Any(p => p.Id.ToString() == publisherId))
            .ToListAsync();

        List<AuthorViewModel> authorsModel = new List<AuthorViewModel>();
        GeneralMapping.MapListToViewModel<Author, AuthorViewModel>(_mapper, authors, authorsModel);

        for (int i = 0; i < authors.Count; i++)
        {
            SetIsUserFollowingAndSubscribed(userId, authors[i], authorsModel[i]);
        }

        return authorsModel;
    }

    public async Task<IEnumerable<AuthorViewModel>> AllAuthorsByUserId(string userId)
    {
        var authors = await _authorRepository
            .GetAll()
            .Include(a => a.AvatarImage)
            .Include(a => a.Followers)
            .Include(a => a.Subscriptions)
            .ThenInclude(s => s.Subscribers)
            .Where(a => a.Followers.Any(u => u.Id.ToString() == userId)
                    || a.Subscriptions.Any(s => s.Subscribers.Any(ss => ss.Id.ToString() == userId)))
            .ToListAsync();

        List<AuthorViewModel> authorsModel = new List<AuthorViewModel>();
        GeneralMapping.MapListToViewModel<Author, AuthorViewModel>(_mapper, authors, authorsModel);

        for (int i = 0; i < authors.Count; i++)
        {
            SetIsUserFollowingAndSubscribed(userId, authors[i], authorsModel[i]);
        }

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

    public async Task DisableAsync(string authorId)
    {
        var author = await _authorRepository.GetSingleByIdAsync(Guid.Parse(authorId));
        author.IsActive = false;

        await _authorRepository.SaveChangesAsync();
    }

    public async Task Edit(AuthorFormModel editedAuthor)
    {
        var authorEntity = await _authorRepository.GetAuthorDetailsByIdAsync(editedAuthor.Id.ToString());

        authorEntity.Alias = editedAuthor.Alias;
        authorEntity.Name = editedAuthor.Name;
        authorEntity.Description = editedAuthor.Description;
        authorEntity.IsActive = editedAuthor.IsActive;
        authorEntity.CategoryID = editedAuthor.CategoryId;
        authorEntity.AvatarImage.URL = editedAuthor.AvatarImageUrl;

        await _authorRepository.SaveChangesAsync();
    }

    public async Task<bool> Exists(string authorId)
    {
        return await _authorRepository.AnyAsync(a => a.Id.ToString() == authorId);
    }

    public async Task FollowAsync(string authorId, string userId)
    {
        var author = await _authorRepository.GetSingleByIdAsync(Guid.Parse(authorId));
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));

        author.Followers.Add(user);
        await _authorRepository.SaveChangesAsync();
    }

    public async Task<FilteredAuthorsServiceModel> GetAllAsync(AllAuthorsQueryModel queryModel, string userId)
    {
        IQueryable<Author> authorsQuery = _authorRepository.GetAll();
        int totalAuthors = authorsQuery.Count();

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            authorsQuery = authorsQuery.Where(a => a.Category.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

            authorsQuery = authorsQuery.Where(a => EF.Functions.Like(a.Alias, wildCard)
                                      || EF.Functions.Like(a.Name, wildCard)
                                      || EF.Functions.Like(a.Description, wildCard));
        }

        authorsQuery = queryModel.SortingOption switch
        {
            AuthorSorting.Newest => authorsQuery.OrderByDescending(a => a.AddedOn),
            AuthorSorting.Oldest => authorsQuery.OrderBy(a => a.AddedOn),
            AuthorSorting.FollowersDescending => authorsQuery.OrderByDescending(a => a.Followers.Count),
            AuthorSorting.FollowersAscending => authorsQuery.OrderBy(a => a.Followers.Count),
            AuthorSorting.SubscribersDescending => authorsQuery.OrderByDescending(a => a.Subscriptions.Count),
            AuthorSorting.SubscribersAscending => authorsQuery.OrderBy(a => a.Subscriptions.Count),
            _ => authorsQuery.Where(a => a.IsActive)
                             .OrderByDescending(a => a.Followers.Count)
                             .ThenByDescending(a => a.AddedOn)
        };

        List<Author> authors = await authorsQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.AuthorsPerPage)
            .Take(queryModel.AuthorsPerPage)
            .Include(a => a.AvatarImage)
            .Include(a => a.Followers)
            .Include(a => a.Subscriptions)
            .ThenInclude(s => s.Subscribers)
            .ToListAsync();

        List<AuthorViewModel> authorsModel = new List<AuthorViewModel>();
        GeneralMapping.MapListToViewModel<Author, AuthorViewModel>(_mapper, authors, authorsModel);

        for (int i = 0; i < authors.Count; i++)
        {
            SetIsUserFollowingAndSubscribed(userId, authors[i], authorsModel[i]);
        }

        return new FilteredAuthorsServiceModel()
        {
            Authors = authorsModel,
            TotalAuthorsCount = totalAuthors,
        };
    }

    public async Task<AuthorFormModel> GetAuthorAsync(string authorId)
    {
        var author = await _authorRepository.GetAuthorByIdWithAvatar(authorId);
        return _mapper.Map<AuthorFormModel>(author);
    }

    public async Task<AuthorDetailsViewModel> GetAuthorDetailsAsync(string authorId, string userId)
    {
        var author = await _authorRepository.GetAuthorDetailsByIdAsync(authorId);
        var authorModel = _mapper.Map<AuthorDetailsViewModel>(author);
        SetIsUserFollowingAndSubscribed(userId, author, authorModel);

        return authorModel;
    }

    public async Task<AuthorSubscribeFormModel> GetAuthorSubscribtionsAsync(string authorId)
    {
        var authorEntity = await _authorRepository.GetAuthorWithSubscriptionsAsync(authorId);

        return _mapper.Map<AuthorSubscribeFormModel>(authorEntity);
    }

    public async Task<bool> IsConnectedPublisher(string authorId, string userId)
    {
        var author = await _authorRepository.GetAuthorWithPublishersAsync(authorId);

        if (!author.Publishers.Any(p => p.UserID.ToString() == userId))
        {
            return false;
        }

        return true;
    }

    public async Task<bool> IsFollowedByUserWithId(string authorId, string userId)
    {
        var author = await _authorRepository.GetAuthorWithFollowersAsync(authorId);
        return author.Followers.Any(a => a.Id.ToString() == userId);
    }


    public async Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors()
    {
        IEnumerable<Author> authors = await _authorRepository.LastThreeAuthors();
        ICollection<AuthorIndexViewModel> allAuthorsModel = new List<AuthorIndexViewModel>();
        GeneralMapping.MapListToViewModel<Author, AuthorIndexViewModel>(_mapper, authors, allAuthorsModel);

        return allAuthorsModel;
    }

    public async Task SubscribeAsync(string authorId, string subscriptionId, string userId)
    {
        var author = await _authorRepository.GetAuthorWithSubscriptionsAndSubscribersAsync(authorId);

        if (HasSubscriptionWithUserIdAndSubscriptionId(author, subscriptionId, userId))
        {
            throw new ArgumentException("Your plan is already set with this subscription.");
        }

        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));
        string oldSubscriptionId = IsSubscribedWithUserId(author, userId);
        if (!string.IsNullOrWhiteSpace(oldSubscriptionId))
        {
            author.Subscriptions
            .FirstOrDefault(s => s.Id.ToString() == oldSubscriptionId)
            .Subscribers
            .Remove(user);
        }

        author.Subscriptions
            .FirstOrDefault(s => s.Id.ToString() == subscriptionId)
            .Subscribers
            .Add(user);

        await _authorRepository.SaveChangesAsync();
    }

    public async Task UnfollowAsync(string authorId, string userId)
    {
        var author = await _authorRepository.GetAuthorWithFollowersAsync(authorId);
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));

        author.Followers.Remove(user);
        await _authorRepository.SaveChangesAsync();
    }

    public async Task UnsubscribeAsync(string authorId, string userId)
    {
        var author = await _authorRepository.GetAuthorWithSubscriptionsAndSubscribersAsync(authorId);
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));

        author
            .Subscriptions
            .FirstOrDefault(s => s.Subscribers.Any(sub => sub.Id.ToString() == userId))
            .Subscribers
            .Remove(user);

        await _authorRepository.SaveChangesAsync();
    }

    private void SetIsUserFollowingAndSubscribed(string userId, Author author, AuthorViewModel authorModel)
    {
        authorModel.IsUserFollowing = author.Followers.Any(u => u.Id.ToString() == userId);
        authorModel.IsUserSubscribed = author.Subscriptions.Any(s => s.Subscribers.Any(ss => ss.Id.ToString() == userId));
    }

    private bool HasSubscriptionWithUserIdAndSubscriptionId(Author author, string subscriptionId, string userId)
    {
        return author
            .Subscriptions
            .FirstOrDefault(s => s.Id.ToString() == subscriptionId)
            .Subscribers
            .Any(sub => sub.Id.ToString() == userId);
    }

    private string IsSubscribedWithUserId(Author author, string userId)
    {
        Subscription oldSubscription = author
            .Subscriptions
            .FirstOrDefault(
            s => s.Subscribers.Any(
                sub => sub.Id.ToString() == userId));

        return oldSubscription == null ? null : oldSubscription.Id.ToString();
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _authorRepository
            .AllAsNoTracking()
            .Where(a => a.IsActive)
            .CountAsync();
    }

    public async Task AddPublisherAsync(string authorId, Publisher publisher)
    {
        var author = await _authorRepository.GetSingleByIdAsync(Guid.Parse(authorId));

        author.Publishers.Add(publisher);
        await _authorRepository.SaveChangesAsync();
    }

    public async Task RemovePublisherAsync(string authorId, Guid publisherId)
    {
        var author = await _authorRepository.GetAuthorWithPublishersAsync(authorId);
        var publisherInstance = author.Publishers.FirstOrDefault(p => p.Id == publisherId);

        author.Publishers.Remove(publisherInstance);
        await _authorRepository.SaveChangesAsync();
    }
}
