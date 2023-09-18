namespace SpiritualHub.Client.ViewModels.Book;

using System.ComponentModel.DataAnnotations;

using Author;
using Category;
using Publisher;

using static Common.EntityValidationConstants.Book;

public class BookFormModel
{
    public BookFormModel()
    {
        this.Authors = new HashSet<AuthorInfoViewModel>();
        this.Categories = new HashSet<CategoryServiceModel>();
        this.Publishers = new HashSet<PublisherInfoViewModel>();
    }

    public string? Id { get; set; } = null!;

    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Short Description")]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Hide book")]
    public bool IsHidden { get; set; }

    [Display(Name = "Book Cover URL")]
    public string ImageUrl { get; set; } = null!;

    [Display(Name = "Choose Author")]
    public string AuthorId { get; set; } = null!;

    [Display(Name = "Choose Category")]
    public int CategoryId { get; set; }

    public IEnumerable<AuthorInfoViewModel> Authors { get; set; }
    
    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    [Display(Name = "Choose Publisher")]
    public string? PublisherId { get; set; } = null!;

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
