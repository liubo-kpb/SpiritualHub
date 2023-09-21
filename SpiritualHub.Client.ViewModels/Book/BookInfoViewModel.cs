namespace SpiritualHub.Client.ViewModels.Book;

public class BookInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsHidden { get; set; }
}
