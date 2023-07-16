namespace SpiritualHub.Data.Models;

public class Blog
{
    public Blog()
    {
        this.Id = Guid.NewGuid();
        this.Comments = new HashSet<Comment>();
        this.Images = new HashSet<Image>();
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public string Text { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public int CagegoryID { get; set; }

    public virtual Category Category { get; set; }

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}
