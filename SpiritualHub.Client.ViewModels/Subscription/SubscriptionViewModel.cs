namespace SpiritualHub.Client.ViewModels.Subscription;

using System.ComponentModel.DataAnnotations;

public class SubscriptionViewModel
{
    public string Id { get; set; }

    public string Price { get; set; }

    [Display(Name = "Type of Subscription")]
    public string SubscriptionTypeName { get; set; }
}
