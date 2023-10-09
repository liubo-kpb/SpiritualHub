namespace SpiritualHub.Client.ViewModels.Book;

using System.ComponentModel.DataAnnotations;

using BaseModels;

using static Common.EntityValidationConstants.Book;

public class BookFormModel : BaseFormModel
{
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Short Description")]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Show Book")]
    public bool IsHidden { get; set; }

    [Display(Name = "Book Cover URL")]
    public string ImageUrl { get; set; } = null!;
}
