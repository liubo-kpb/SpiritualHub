namespace SpiritualHub.Data.Models;

public class Publisher
{
    public Publisher()
    {
        this.Id = Guid.NewGuid();
        this.PublishedAuthors = new HashSet<Author>();
    }

    public Guid Id { get; set; }

    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Author> PublishedAuthors { get; set; }
}
