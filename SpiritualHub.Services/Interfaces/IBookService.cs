namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Book;
using Models.Book;

public interface IBookService
{
    Task<int>                           GetAllCountAsync();

    Task<BookDetailsViewModel>          GetBookDetailsAsync(string id, string userId);

    Task<BookFormModel>                 GetBookInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredBooksServiceModel>     GetAllAsync(AllBooksQueryModel queryModel, string userId);

    Task<IEnumerable<BookViewModel>>    AllBooksByUserIdAsync(string userId);

    Task<IEnumerable<BookViewModel>>    GetBooksByPublisherIdAsync(string publisherId, string userId);

    Task<string>                        GetAuthorIdAsync(string bookId);

    Task<string>                        CreateAsync(BookFormModel newBook);

    Task                                EditAsync(BookFormModel updatedbook);

    Task                                DeleteAsync(string bookId);

    Task                                HideAsync(string bookId);

    Task                                UnideAsync(string bookId);

    Task<bool>                          HasBookAsync(string bookId, string userId);

    Task                                GetAsync(string bookId, string userId);

    Task                                RemoveAsync(string bookId, string userId);
}
