namespace SpiritualHub.Client.ViewModels.Event;

using SpiritualHub.Client.ViewModels.Publisher;

public class EventDetailsViewModel : EventViewModel
{
    public PublisherInfoViewModel Publisher { get; set; } = null!;
}
