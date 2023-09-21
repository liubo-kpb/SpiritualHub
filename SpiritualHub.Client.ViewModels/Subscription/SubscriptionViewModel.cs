namespace SpiritualHub.Client.ViewModels.Subscription;

using System.ComponentModel.DataAnnotations;

public class SubscriptionViewModel
{
    public string Id { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Type of Subscription")]
    public string SubscriptionType { get; set; } = null!;
}
