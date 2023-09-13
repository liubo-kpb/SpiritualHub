namespace SpiritualHub.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidationConstants.Book;

public class Book
{

    public Book()
    {
        this.Id = Guid.NewGuid();
        this.Ratings = new HashSet<Rating>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    public DateTime AddedOn { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; } = null!;

    public Guid ImageID { get; set; }

    public virtual Image Image { get; set; } = null!;

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    public int CategoryID { get; set; }

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; }
}
