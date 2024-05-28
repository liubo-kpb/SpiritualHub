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

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is BookFormModel other
            && this.Id == other.Id
            && this.Title == other.Title
            && this.Description == other.Description
            && this.ShortDescription == other.ShortDescription
            && this.Price == other.Price
            && this.IsHidden == other.IsHidden
            && this.ImageUrl == other.ImageUrl)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
