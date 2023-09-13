namespace SpiritualHub.Services.Models.Book;

using Client.ViewModels.Book;

public class FilteredBooksServiceModel
{
    public FilteredBooksServiceModel()
    {
        this.Books = new HashSet<BookViewModel>();
    }

    public int TotalBooksCount { get; set; }

    public IEnumerable<BookViewModel> Books { get; set; }
}
