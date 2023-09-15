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

    public string Id { get; set; } = null!;

    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = null!;

    public Guid AuthorID { get; set; }

    public int CategoryID { get; set; }

    public IEnumerable<AuthorInfoViewModel> Authors { get; set; }
    
    public IEnumerable<CategoryServiceModel> Categories { get; set; }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
