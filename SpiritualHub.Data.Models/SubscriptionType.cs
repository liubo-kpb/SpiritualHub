namespace SpiritualHub.Data.Models;

public class SubscriptionType
{
    public SubscriptionType()
    {
        this.Subscriptions = new HashSet<Subscription>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set;}
}

public enum SubTypeConstraint
{
    Montly,
    Quarterly,
    Anual
}