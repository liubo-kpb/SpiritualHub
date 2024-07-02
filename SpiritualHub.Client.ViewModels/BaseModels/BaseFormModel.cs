namespace SpiritualHub.Client.ViewModels.BaseModels;

using System.ComponentModel.DataAnnotations;

using Category;
using Publisher;

public class BaseFormModel
{
    public BaseFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
        this.Publishers = new HashSet<PublisherInfoViewModel>();
    }

    public string? Id { get; set; } = null!;

    [Display(Name = "Choose Category:")]
    public int CategoryId { get; set; }


    [Display(Name = "Choose Publisher:")]
    public string? PublisherId { get; set; }

    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
