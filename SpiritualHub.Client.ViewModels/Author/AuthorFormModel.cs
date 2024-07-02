namespace SpiritualHub.Client.ViewModels.Author;

using System.ComponentModel.DataAnnotations;

using BaseModels;

using static Common.EntityValidationConstants.Author;

public class AuthorFormModel : BaseFormModel
{
    public AuthorFormModel()
    {
        IsActive = true;
    }

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
}
