namespace SpiritualHub.Client.ViewModels.Event;

using System.ComponentModel.DataAnnotations;

using Author;
using Category;

using static Common.EntityValidationConstants.Event;

public class EventFormModel
{
    public EventFormModel()
    {
        this.Categories = new HashSet<CategoryServiceModel>();
        this.Authors = new HashSet<AuthorInfoViewModel>();
        IsOnline = true;
    }
    public Guid Id { get; set; }

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
    [Display(Name = "Choose Start Date and Time")]
    public DateTime StartDateTime { get; set; }

    [Required]
    [Display(Name = "Choose End Date and Time")]
    public DateTime EndDateTime { get; set; }

    [Display(Name = "Provide location name")]
    public string? LocationName { get; set; }

    [Display(Name = "Link to Location")]
    public string? LocationUrl { get; set; }

    [Required]
    [Display(Name = "Event Image URL")]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    [Required]
    [Display(Name = "Author")]
    public string AuthorId { get; set; } = null!;

    public IEnumerable<AuthorInfoViewModel> Authors { get; set; }
}
