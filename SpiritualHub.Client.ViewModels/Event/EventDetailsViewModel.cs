namespace SpiritualHub.Client.ViewModels.Event;

using SpiritualHub.Client.ViewModels.Publisher;

public class EventDetailsViewModel : EventViewModel
{
    public EventDetailsViewModel()
    {
        this.Publishers = new HashSet<PublisherInfoViewModel>();
    }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
