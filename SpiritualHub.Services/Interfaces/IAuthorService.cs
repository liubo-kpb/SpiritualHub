namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;
using Services.Models.Author;
using Data.Models;

public interface IAuthorService
{
    Task<FilteredAuthorsServiceModel> GetAllAsync(AllAuthorsQueryModel queryModel, string userId);

    Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors();

    Task<string> CreateAuthor(AuthorFormModel newAuthor, Publisher publisher);

    Task<IEnumerable<AuthorViewModel>> AllAuthorsByPublisherId(string userId, string publisherId);

    Task<IEnumerable<AuthorViewModel>> AllAuthorsByUserId(string userId);

    Task<AuthorDetailsViewModel> GetAuthorDetailsAsync(string authorId, string userId);

    Task<bool> Exists(string authorId);

    Task Edit(AuthorFormModel editedAuthor);

    Task<bool> HasConnectedPublisher(string authorId, string userId);

    Task<AuthorFormModel> GetAuthorAsync(string authorId);

    Task DisableAsync(string authorId);

    Task<bool> IsFollowedByUserWithId(string authorId, string userId);

    Task FollowAsync(string authorId, string userId);

    Task SubscribeAsync(string authorId, string subscriptionId, string userId);

    Task<AuthorSubscribeFormModel> GetAuthorSubscribtionsAsync(string authorId);

    Task UnfollowAsync(string authorId, string userId);

    Task UnsubscribeAsync(string authorId, string userId);
}
