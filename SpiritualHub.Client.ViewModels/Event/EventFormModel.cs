namespace SpiritualHub.Client.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using Author;
using Category;
using Publisher;

using static Common.EntityValidationConstants.Event;

public class EventFormModel
{
    public EventFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
        this.Authors = new HashSet<AuthorInfoViewModel>();
        this.Publishers = new HashSet<PublisherInfoViewModel>();
        IsOnline = true;
    }

    public string? Id { get; set; } = null!;

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

    [Required]
    [Display(Name = "Choose Category:")]
    public int CategoryId { get; set; }

    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    [Required]
    [Display(Name = "Choose Author:")]
    public string AuthorId { get; set; } = null!;

    public IEnumerable<AuthorInfoViewModel> Authors { get; set; }

    [Display(Name = "Choose Publisher:")]
    public string? PublisherId { get; set; }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
