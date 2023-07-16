namespace SpiritualHub.Data.Models;
public class Author
{
    public Author()
    {
        this.Id = Guid.NewGuid();
        this.Followers = new HashSet<ApplicationUser>();
        this.Subscribers = new HashSet<ApplicationUser>();

        foreach (var rating in this.Ratings)
        {
            this.OverallRating += rating.Stars;
        }

        this.OverallRating /= this.Ratings.Count;
    }

    public Guid Id { get; set; }
    
    public string Alias { get; set; }
    
    public string Name { get; set; }
    
    public float OverallRating { get; set; }

    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public Guid AvatarImageID { get; set; }

    public virtual Image AvatarImage { get; set; }

    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<ApplicationUser> Followers { get; set; }
    
    public virtual ICollection<ApplicationUser> Subscribers { get; set; }
    
    public virtual ICollection<string> Content { get; set; }
}
