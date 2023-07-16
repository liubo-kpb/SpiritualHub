namespace SpiritualHub.Data.Models;

public class Rating
{
    public Rating()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public int Stars { get; set; }

    public string Text { get; set; }

    public Guid CourseID { get; set; }

    public virtual Course Course { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; }
}
