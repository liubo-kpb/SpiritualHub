namespace SpiritualHub.Client.ViewModels.Book;

using System.ComponentModel.DataAnnotations;

using Infrastructure.Enums;

using static Common.GeneralApplicationConstants;

public class AllBooksQueryModel
{
    public AllBooksQueryModel()
    {
        this.Categories = new HashSet<string>();
        this.Books = new HashSet<BookViewModel>();

        CurrentPage = DefaultPage;
        BooksPerPage = EntitiesPerPage;
    }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Search by text")]
    public string SearchTerm { get; set; } = null!;

    [Display(Name = "Sort by")]
    public BookSorting SortingOption { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Show on Page")]
    public int BooksPerPage { get; set; }

    public int TotalBooksCount { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<BookViewModel> Books { get; set; }
}
