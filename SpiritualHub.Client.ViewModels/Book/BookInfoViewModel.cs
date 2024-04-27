namespace SpiritualHub.Client.ViewModels.Book;

using SpiritualHub.Client.ViewModels.Publisher;

public class BookInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsHidden { get; set; }

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is BookInfoViewModel other)
        {
            if (this.Id == other.Id
                && this.Title == other.Title
                && this.ShortDescription == other.ShortDescription
                && this.Price == other.Price
                && this.IsHidden == other.IsHidden)
            {
                result = true;
            }
        }

        return result;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
