namespace SpiritualHub.Client.ViewModels.Event;

using Author;

public class EventViewModel
{

    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsOnline { get; set; }

    public bool IsUserJoined { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string LocationName { get; set; } = null!;

    public string LocationUrl { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string ImageURL { get; set; } = null!;

    public string Participation { get; set; } = null!;

    public AuthorInfoViewModel Author { get; set; } = null!;
}
