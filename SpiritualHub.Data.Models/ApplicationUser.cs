namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

using static Common.EntityValidationConstants.ApplicationUser;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        this.Id = Guid.NewGuid();
        this.Comments = new HashSet<Comment>();
        this.Ratings = new HashSet<Rating>();
        this.JoinedEvents = new HashSet<Event>();
        this.Books = new HashSet<Book>();
        this.Courses = new HashSet<Course>();
        this.FollowedAuthors = new HashSet<Author>();
        this.Subscriptions = new HashSet<Subscription>();
    }

    [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
    public string FirstName { get; set; } = null!;

    [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
    public string LastName { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Event> JoinedEvents { get; set; }

    public virtual ICollection<Book> Books { get; set; }

    public virtual ICollection<Course> Courses { get; set; }

    public virtual ICollection<Author> FollowedAuthors { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }
}
