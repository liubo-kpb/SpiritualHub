namespace SpiritualHub.Data.Models;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
            this.Id = Guid.NewGuid();
    }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<Event> JoinedEvents { get; set; }

    public virtual ICollection<Author> FollowingAuthors { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; }
}
