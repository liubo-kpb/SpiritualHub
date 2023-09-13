namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Book;
using Models.Book;

public interface IBookService
{
    Task<int>                           GetAllCountAsync();

    Task<BookDetailsViewModel>          GetBookDetailsAsync(string id, string userId);

    Task<BookFormModel>                 GetEventInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredBooksServiceModel>    GetAllAsync(AllBooksQueryModel queryModel);

    Task<IEnumerable<BookViewModel>>    AllBooksByUserIdAsync(string userId);

    Task<IEnumerable<BookViewModel>>    GetBooksByPublisherIdAsync(string publisherId);

    Task<string>                        GetAuthorIdAsync(string bookId);

    Task<string>                        CreateAsync(BookFormModel newEvent);

    Task                                EditAsync(BookFormModel updatedEvent);

    Task                                DeleteAsync(string bookId);

    Task<bool>                          IsAddedAsync(string bookId, string userId);

    Task                                AddAsync(string bookId, string userId);

    Task                                RemoveAsync(string bookId, string userId);
}
