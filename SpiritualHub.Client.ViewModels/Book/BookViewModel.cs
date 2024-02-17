namespace SpiritualHub.Client.ViewModels.Book;

using BaseModels;
using Author;


public class BookViewModel : BaseDetailsViewModel
{
    public string Title { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime AddedOn { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public bool HasBook { get; set; }

    public bool IsHidden { get; set; }

    public AuthorInfoViewModel Author { get; set; } = null!;
}
