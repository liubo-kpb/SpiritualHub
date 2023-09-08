namespace SpiritualHub.Client.ViewModels.Event;

public class EventInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public decimal Price { get; set; }
}
