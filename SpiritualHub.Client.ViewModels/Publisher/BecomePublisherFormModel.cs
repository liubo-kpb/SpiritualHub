namespace SpiritualHub.Client.ViewModels.Publisher;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Publisher;

public class BecomePublisherFormModel
{
    [Required]
    [MaxLength(PhoneNumberMaxLength)]
    [Display(Name = "Phone Number")]
    [Phone]
    public string PhoneNumber { get; set; } = null!;
}
