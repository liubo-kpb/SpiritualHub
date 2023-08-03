namespace SpiritualHub.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static SpiritualHub.Common.EntityValidationConstants.Book;

public class Book
{

    public Book()
    {
        this.Id = Guid.NewGuid();
        this.Ratings = new HashSet<Rating>();
        this.Images = new HashSet<Image>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; }

    public string Description { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}
