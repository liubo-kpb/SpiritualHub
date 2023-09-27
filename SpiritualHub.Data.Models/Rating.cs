namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Rating;

public class Rating
{
    public Rating()
    {
        this.Id = Guid.NewGuid();
        this.Authors = new HashSet<Author>();
        this.Events = new HashSet<Event>();
        this.Books = new HashSet<Book>();
        this.Courses = new HashSet<Course>();
        this.Modules = new HashSet<Module>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [Range(MinStar, MaxStar)]
    public int Stars { get; set; }

    [Required]
    [StringLength(HeadlineMaxLength)]
    public string Headline { get; set; } = null!;

    [StringLength(TextMaxLength)]
    public string Text { get; set; } = null!;

    [Required]
    public Guid UserID { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    public ICollection<Author> Authors { get; set; }

    public ICollection<Event> Events { get; set; }

    public ICollection<Book> Books { get; set; }

    public ICollection<Course> Courses { get; set; }

    public ICollection<Module> Modules { get; set; }
}
