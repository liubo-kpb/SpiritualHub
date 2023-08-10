namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using static SpiritualHub.Common.EntityValidationConstants.Blog;

public class Blog
{
    public Blog()
    {
        this.Id = Guid.NewGuid();
        this.Comments = new HashSet<Comment>();
       this.Images = new HashSet<BlogPostImage>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; }

    [Required]
    [StringLength(ShortDesciptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    public string ShortDescription { get; set; }

    [Required]
    public string Text { get; set; }

    [Required]
    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    [Required]
    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    [Required]
    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<BlogPostImage> Images { get; set; }
}
