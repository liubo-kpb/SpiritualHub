namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;
using Services.Models.Author;
using Data.Models;

public interface IAuthorService
{
    Task<FilteredAuthorsServiceModel>       GetAllAsync(AllAuthorsQueryModel queryModel, string userId);

    Task<IEnumerable<AuthorInfoViewModel>>  GetAllAsync();

    Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors();

    Task<string>                            CreateAuthor(AuthorFormModel newAuthor, Publisher publisher);

    Task<IEnumerable<AuthorViewModel>>      AllAuthorsByPublisherIdAsync(string userId, string publisherId);

    Task<IEnumerable<AuthorViewModel>>      AllAuthorsByUserId(string userId);

    Task<AuthorDetailsViewModel>            GetAuthorDetailsAsync(string authorId, string userId);

    Task<bool>                              Exists(string authorId);

    Task                                    EditAsync(AuthorFormModel editedAuthor);

    Task<AuthorFormModel>                   GetAuthorAsync(string authorId);

    Task                                    ActivateAsync(string authorId);

    Task                                    DisableAsync(string authorId);

    Task<bool>                              IsFollowedByUserWithId(string authorId, string userId);

    Task                                    FollowAsync(string authorId, string userId);

    Task                                    SubscribeAsync(string authorId, string subscriptionId, string userId);

    Task<AuthorSubscribeFormModel>          GetAuthorSubscribtionsAsync(string authorId);

    Task                                    UnfollowAsync(string authorId, string userId);

    Task                                    UnsubscribeAsync(string authorId, string userId);

    Task<int>                               GetAllCountAsync();

    Task                                    AddPublisherAsync(string authorId, Publisher publisher);

    Task                                    RemovePublisherAsync(string authorId, Guid publisherId);
}
