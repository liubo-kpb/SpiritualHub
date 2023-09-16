namespace SpiritualHub.Client.ViewModels.Author;

using System.ComponentModel.DataAnnotations;

using Category;

using static Common.EntityValidationConstants.Author;

public class AuthorFormModel
{
    public AuthorFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
        IsActive = true;
    }
    public string? Id { get; set; } = null!;

    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Alias { get; set; } = null!;

    [Required]
    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Display(Name = "Is Author Active:")]
    public bool IsActive { get; set; }

    [Required]
    [Display(Name = "Author Image URL:")]
    public string AvatarImageUrl { get; set; } = null!;

    [Required]
    [Display(Name = "Choose Category:")]
    public int CategoryId { get; set; }

    public IEnumerable<CategoryServiceModel> Categories { get; set; }
}
