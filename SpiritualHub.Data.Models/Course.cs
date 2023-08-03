namespace SpiritualHub.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SpiritualHub.Common.EntityValidationConstants.Course;

public class Course
{
    public Course()
    {
        this.Id = Guid.NewGuid();
        this.Ratings = new HashSet<Rating>();
        this.Images = new HashSet<Image>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; }

    [Required]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; }

    [Required]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    [Required]
    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    [Required]
    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}
