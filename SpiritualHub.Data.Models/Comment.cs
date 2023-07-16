namespace SpiritualHub.Data.Models;

public class Comment
{
    public Comment()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Text { get; set; }

    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; }

    public Guid PostID { get; set; }

    public virtual Blog Post { get; set; }
}
