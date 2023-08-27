namespace SpiritualHub.Client.ViewModels.Event;

using Author;

public class EventViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsOnline { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ImageURL { get; set; } = null!;

    public AuthorInfoViewModel AuthorAlias { get; set; } = null!;
}
