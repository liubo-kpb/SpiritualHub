namespace SpiritualHub.Client.ViewModels.Category;

using System.ComponentModel.DataAnnotations;

public class AllCategoryQueryModel
{
    public AllCategoryQueryModel()
    {
        Categories = new HashSet<CategoryServiceModel>(); ;
    }

    [Display(Name = "Search by Name:")]
    public string SearchTerm { get; set; } = null!;

    public IEnumerable<CategoryServiceModel> Categories { get; set; }
}
