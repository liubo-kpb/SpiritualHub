namespace SpiritualHub.Client.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using ViewModels.BaseModels;

using static Common.EntityValidationConstants.Event;

public class EventFormModel : BaseFormModel
{
    public EventFormModel()
    {
        IsOnline = true;
    }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Will there be a Livestream?")]
    public bool IsOnline { get; set; }

    public decimal Price { get; set; }

    [Required]
    [Display(Name = "Set Starting Date and Time:")]
    public DateTime StartDateTime { get; set; }

    [Required]
    [Display(Name = "Set End Date and Time:")]
    public DateTime EndDateTime { get; set; }

    [Display(Name = "Provide Location Name:")]
    public string? LocationName { get; set; }

    [Display(Name = "Link to Location:")]
    public string? LocationUrl { get; set; }

    [Required]
    [Display(Name = "Event Image URL:")]
    public string ImageUrl { get; set; } = null!;
}
