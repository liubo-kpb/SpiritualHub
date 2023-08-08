namespace SpiritualHub.Client.ViewModels.Author;

using SpiritualHub.Client.ViewModels.Category;
using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Author;

public class AuthorFormModel
{
    public AuthorFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
    }

    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Alias { get; set; } = null!;

    [Required]
    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Author Image URL")]
    public string AvatarImageUrl { get; set; } = null!;

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<CategoryServiceModel> Categories { get; set; }
}
