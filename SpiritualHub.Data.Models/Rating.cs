namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Rating
{
    public Rating()
    {
        this.Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public int Stars { get; set; }

    public string Text { get; set; }

    [Required]
    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public Guid CourseID { get; set; }

    public virtual Course Course { get; set; }

    public Guid EventID { get; set; }

    public virtual Event Event { get; set; }

    public Guid BookID { get; set; }

    public virtual Book Book { get; set; }
}
