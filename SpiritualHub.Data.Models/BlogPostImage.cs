namespace SpiritualHub.Data.Models;

public class BlogPostImage
{
    public Guid BlogID { get; set; }

    public Blog Blog { get; set; } = null!;

    public Guid ImageID { get; set; }

    public Image Image { get; set; } = null!;
}
