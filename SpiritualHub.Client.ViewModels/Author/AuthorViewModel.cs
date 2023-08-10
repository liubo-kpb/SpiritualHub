namespace SpiritualHub.Client.ViewModels.Author;

using System.ComponentModel.DataAnnotations;

using Data.Models;

public class AuthorViewModel
{
    public AuthorViewModel()
    {
        this.Followers = new HashSet<ApplicationUser>();
        this.Subscribers = new HashSet<ApplicationUser>();

        this.FollowerCount = this.Followers.Count;
        this.SubscriberCount = this.Subscribers.Count;
    }

    public Guid Id { get; set; }

    public string Alias { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Avatar Image")]
    public string AvatarImageUrl { get; set; } = null!;

    [Display(Name = "Followers")]
    public int FollowerCount { get; set; }

    public ICollection<ApplicationUser> Followers { get; set; }

    [Display(Name = "Subscribers")]
    public int SubscriberCount { get; set; }

    public ICollection<ApplicationUser> Subscribers { get; set; }
}
