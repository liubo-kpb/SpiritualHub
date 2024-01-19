namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Module;

public class Module
{
    public Module()
    {
        Id = Guid.NewGuid();
        this.Ratings = new HashSet<Rating>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public int Number { get; set; }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public string VideoUrl { get; set; } = null!;

    public string Text { get; set; } = null!;

    public bool IsActive { get; set; }

    [Required]
    public Guid CourseID { get; set; }

    public Course Course { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; }
}
