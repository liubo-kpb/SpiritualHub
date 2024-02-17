namespace SpiritualHub.Client.ViewModels.Author;

using System.ComponentModel.DataAnnotations;

using BaseModels;

public class AuthorViewModel : BaseDetailsViewModel
{
    public string Alias { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Avatar Image")]
    public string AvatarImageUrl { get; set; } = null!;

    [Display(Name = "Followers")]
    public int FollowerCount { get; set; }

    public bool IsUserFollowing { get; set; }

    [Display(Name = "Subscribers")]
    public int SubscriberCount { get; set; }

    public bool IsUserSubscribed { get; set; }
}
