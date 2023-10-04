namespace SpiritualHub.Client.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using Infrastructure.Enums;

using static Common.GeneralApplicationConstants;

public class AllEventsQueryModel
{
    public AllEventsQueryModel()
    {
        this.Categories = new HashSet<string>();
        this.Events = new HashSet<EventViewModel>();

        CurrentPage = DefaultPage;
        EventsPerPage = EntitiesPerPageConstant;
    }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Search by text")]
    public string SearchTerm { get; set; } = null!;

    [Display(Name = "Sort by")]
    public EventSorting SortingOption { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Show on Page")]
    public int EventsPerPage { get; set; }

    public int TotalEventsCount { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<EventViewModel> Events { get; set; }
}