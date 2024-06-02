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

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is BookViewModel other
            && this.Id == other.Id
            && this.Title == other.Title
            && this.ShortDescription == other.ShortDescription
            && this.Price == other.Price
            && this.AddedOn == other.AddedOn
            && this.ImageUrl == other.ImageUrl
            && this.CategoryName == other.CategoryName
            && this.IsHidden == other.IsHidden)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
