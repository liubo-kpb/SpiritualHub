namespace SpiritualHub.Data.Models;

public class Image
{
    public Image()
    {
        this.Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string URL { get; set; }

    public Guid CourseID { get; set; }

    public virtual Course Course { get; set; }

    public Guid BlogID { get; set; }

    public virtual Blog Blog { get; set; }
}
