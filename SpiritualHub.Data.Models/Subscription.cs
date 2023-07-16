namespace SpiritualHub.Data.Models;

public class Subscription
{
    public Subscription()
    {
        this.Id = Guid.NewGuid();
        this.Users = new HashSet<ApplicationUser>();
    }

    public Guid Id { get; set; }

    public decimal Price { get; set; }

    public int SubscriptionTypeID { get; set; }

    public virtual SubscriptionType SubscriptionType { get; set; }

    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

   public virtual ICollection<ApplicationUser> Users { get; set; }
}
