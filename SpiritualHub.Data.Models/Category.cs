namespace SpiritualHub.Data.Models;

public class Category
{
    public Category()
    {
        this.Authors = new HashSet<Author>();
        this.Events = new HashSet<Event>();
        this.Blogs = new HashSet<Blog>();
        this.Courses = new HashSet<Course>();
        this.Books = new HashSet<Book>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Author> Authors { get; set; }

    public virtual ICollection<Event> Events { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
