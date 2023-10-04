namespace SpiritualHub.Client.ViewModels.BaseModels;

using System.ComponentModel.DataAnnotations;

using Author;
using Category;
using Publisher;

public class BaseFormModel
{
    public BaseFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
        this.Authors = new HashSet<AuthorInfoViewModel>();
        this.Publishers = new HashSet<PublisherInfoViewModel>();
    }

    public string? Id { get; set; } = null!;

    [Display(Name = "Choose Category:")]
    public int CategoryId { get; set; }

    [Display(Name = "Choose Author:")]
    public string AuthorId { get; set; } = null!;

    [Display(Name = "Choose Publisher:")]
    public string PublisherId { get; set; } = null!;

    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    public IEnumerable<AuthorInfoViewModel> Authors { get; set; }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
