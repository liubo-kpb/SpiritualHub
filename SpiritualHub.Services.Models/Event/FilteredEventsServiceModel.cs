namespace SpiritualHub.Services.Models.Event;

using Client.ViewModels.Event;

public class FilteredEventsServiceModel
{
    public FilteredEventsServiceModel()
    {
        this.Events = new HashSet<EventViewModel>();
    }

    public int TotalEventsCount { get; set; }

    public IEnumerable<EventViewModel> Events { get; set; }
}
