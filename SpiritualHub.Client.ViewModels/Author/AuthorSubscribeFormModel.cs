namespace SpiritualHub.Client.ViewModels.Author;

using SpiritualHub.Client.ViewModels.Subscription;

public class AuthorSubscribeFormModel : AuthorViewModel
{
    public AuthorSubscribeFormModel()
    {
        this.Subscriptions = new HashSet<SubscriptionViewModel>();
    }

    public string SubscriptionId { get; set; } = null!;

    public ICollection<SubscriptionViewModel> Subscriptions { get; set; }
}
