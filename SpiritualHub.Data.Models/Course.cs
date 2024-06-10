namespace SpiritualHub.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidationConstants.Course;

public class Course
{
    public Course()
    {
        this.Id = Guid.NewGuid();
        this.Modules = new List<Module>();
        this.Ratings = new HashSet<Rating>();
        this.Students = new HashSet<ApplicationUser>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

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

    public bool IsActive { get; set; } = true;

    [Required]
    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; } = null!;

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    [Required]
    public int CategoryID { get; set; }

    public virtual Category? Category { get; set; } = null!;
    
    public virtual Guid ImageID { get; set; }

    public virtual Image Image { get; set; } = null!;

    public virtual ICollection<Module> Modules { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<ApplicationUser> Students { get; set; }

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is Course other
            && this.Id == other.Id
            && this.Name == other.Name
            && this.ShortDescription == other.ShortDescription
            && this.Description == other.Description
            && this.Price == other.Price
            && this.IsActive == other.IsActive)
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
