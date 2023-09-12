namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Book;
using Models.Event;

public interface IBookService
{
    Task<int>                           GetAllCountAsync();

    Task<BookDetailsViewModel>          GetBookDetailsAsync(string id, string userId);

    Task<BookFormModel>                 GetEventInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredEventsServiceModel>    GetAllAsync(AllBooksQueryModel queryModel, string userId);

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
