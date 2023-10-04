namespace SpiritualHub.Client.ViewModels.BaseModels;

using System.ComponentModel.DataAnnotations;


using static Common.GeneralApplicationConstants;

public class BaseQueryModel<TViewModel, TSortingEnum>
    where TViewModel : class
    where TSortingEnum : Enum
{
    public BaseQueryModel()
    {
        this.Categories = new HashSet<string>();
        this.EntityViewModels = new HashSet<TViewModel>();

        CurrentPage = DefaultPage;
        EntitiesPerPage = EntitiesPerPageConstant;
    }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Search by text")]
    public string SearchTerm { get; set; } = null!;

    [Display(Name = "Sort by")]
    public TSortingEnum? SortingOption { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Show on Page")]
    public int EntitiesPerPage { get; set; }

    public int TotalEntitiesCount { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<TViewModel> EntityViewModels { get; set; }
}
