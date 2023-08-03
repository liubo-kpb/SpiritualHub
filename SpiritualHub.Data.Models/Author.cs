namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static SpiritualHub.Common.EntityValidationConstants.Author;

public class Author
{
    public Author()
    {
        this.Id = Guid.NewGuid();
        this.Followers = new HashSet<ApplicationUser>();
        this.Subscriptions = new HashSet<Subscription>();
        this.Ratings = new HashSet<Rating>();
    }

    [Key]
    public Guid Id { get; set; }

    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Alias { get; set; }

    [Required]
    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Name { get; set; }

    [Required]
    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public Guid AvatarImageID { get; set; }

    public virtual Image AvatarImage { get; set; }

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public virtual ICollection<ApplicationUser> Followers { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }

    public virtual ICollection<Book> Books { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}
