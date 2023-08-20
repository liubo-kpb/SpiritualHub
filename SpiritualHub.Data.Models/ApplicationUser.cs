namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        this.FollowedAuthors = new HashSet<Author>();
        this.Subscriptions = new HashSet<Subscription>();
    }

    [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
    public string FirstName { get; set; }

    [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
    public string LastName { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Event> JoinedEvents { get; set; }

    public virtual ICollection<Author> FollowedAuthors { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }
}
