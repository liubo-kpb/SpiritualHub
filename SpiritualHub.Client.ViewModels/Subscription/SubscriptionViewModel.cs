namespace SpiritualHub.Client.ViewModels.Subscription;

using SpiritualHub.Client.ViewModels.Publisher;
using System.ComponentModel.DataAnnotations;

public class SubscriptionViewModel
{
    public string Id { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Type of Subscription")]
    public string SubscriptionType { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is SubscriptionViewModel other)
        {
            if (this.Id == other.Id
                && this.SubscriptionType == other.SubscriptionType
                && this.Price == other.Price)
            {
                result = true;
            }
        }

        return result;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
