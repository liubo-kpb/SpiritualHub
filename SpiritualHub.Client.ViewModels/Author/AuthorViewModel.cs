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

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is AuthorViewModel other)
        {
            if (this.Id == other.Id
                && this.Alias == other.Alias
                && this.Name == other.Name
                && this.Description == other.Description
                && this.IsActive == other.IsActive
                && this.CategoryName == other.CategoryName
                && this.AvatarImageUrl == other.AvatarImageUrl
                && this.FollowerCount == other.FollowerCount
                && this.IsUserFollowing == other.IsUserFollowing
                && this.SubscriberCount == other.SubscriberCount
                && this.IsUserSubscribed == other.IsUserSubscribed)
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
