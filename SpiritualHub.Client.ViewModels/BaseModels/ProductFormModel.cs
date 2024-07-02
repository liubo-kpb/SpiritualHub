namespace SpiritualHub.Client.ViewModels.BaseModels;

using Author;
using System.ComponentModel.DataAnnotations;

public class ProductFormModel : BaseFormModel
{
    public ProductFormModel()
        : base()
    {
        this.Authors = new HashSet<AuthorInfoViewModel>();
    }

    [Display(Name = "Choose Author:")]
    public virtual string AuthorId { get; set; } = null!;

    public virtual IEnumerable<AuthorInfoViewModel> Authors { get; set; }
}
