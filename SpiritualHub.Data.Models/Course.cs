namespace SpiritualHub.Data.Models;

public class Course
{
    public Course()
    {
        this.Id = Guid.NewGuid();
        this.Ratings = new HashSet<Rating>();
        this.Images = new HashSet<Image>();
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ShortDescription { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public int CagegoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Image> Images { get; set; }
}
