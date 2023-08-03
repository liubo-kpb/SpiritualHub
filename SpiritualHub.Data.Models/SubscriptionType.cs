namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class SubscriptionType
{
    public SubscriptionType()
    {
        this.Subscriptions = new HashSet<Subscription>();
    }

    [Key]
    public int Id { get; set; }

    public string Type { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set;}
}