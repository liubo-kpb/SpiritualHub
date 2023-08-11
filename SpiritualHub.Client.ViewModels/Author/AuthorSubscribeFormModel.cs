namespace SpiritualHub.Client.ViewModels.Author;

using SpiritualHub.Client.ViewModels.Subscription;

public class AuthorSubscribeFormModel
{
    public AuthorSubscribeFormModel()
    {
        this.Subscriptions = new HashSet<SubscriptionViewModel>();
    }
    public string Id { get; set; } = null!;

    public string Alias { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AvatarImageUrl { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string SubscriptionId { get; set; } = null!;

    public ICollection<SubscriptionViewModel> Subscriptions { get; set; }
}
