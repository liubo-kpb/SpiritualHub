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
        this.Publishers = new HashSet<Publisher>();
        this.Books = new HashSet<Book>();
        this.Events = new HashSet<Event>();
        this.Courses = new HashSet<Course>();
        this.Blogs = new HashSet<Blog>();
        this.Comments = new HashSet<Comment>();
    }

    [Key]
    public Guid Id { get; set; }

    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Alias { get; set; } = null!;

    [Required]
    [StringLength(AliasMaxLength, MinimumLength = AliasMinLength)]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime AddedOn { get; set; }

    public bool IsActive { get; set; }

    [Required]
    public int CategoryID { get; set; }

    public virtual Category? Category { get; set; } = null!;

    public Guid AvatarImageID { get; set; }

    public virtual Image AvatarImage { get; set; } = null!;

    public virtual ICollection<Publisher> Publishers { get; set; }

    public virtual ICollection<ApplicationUser> Followers { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }

    public virtual ICollection<Book> Books { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}
