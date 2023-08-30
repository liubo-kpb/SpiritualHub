namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Rating;

public class Rating
{
    public Rating()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(RatingMinStar, RatingMaxStar)]
    public int Stars { get; set; }

    public string Text { get; set; } = null!;

    [Required]
    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; } = null!;

    public Guid CourseID { get; set; }

    public virtual Course Course { get; set; } = null!;

    public Guid EventID { get; set; }

    public virtual Event Event { get; set; } = null!;

    public Guid BookID { get; set; }

    public virtual Book Book { get; set; } = null!;
}
