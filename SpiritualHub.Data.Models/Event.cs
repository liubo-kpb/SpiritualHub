namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidationConstants.Event;

public class Event
{
    public Event()
    {
        this.Id             = Guid.NewGuid();
        // this.Discounts      = new HashSet<string>();
        this.Participants   = new HashSet<ApplicationUser>();
        this.Ratings   = new HashSet<Rating>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime StartDateTime { get; set;}

    [Required]
    public DateTime EndDateTime { get; set;}

    public string? LocationName { get; set; } = null!;

    public string? LocationUrl { get; set; } = null!;

    [Required]
    public bool IsOnline { get; set; }

    [Required]
    public int CategoryID { get; set; }

    public virtual Category? Category { get; set; } = null!;

    [Required]
    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Guid ImageID { get; set; }

    public virtual Image Image { get; set; } = null!;
    
    public Guid PublisherID { get; set; }

    public virtual Publisher Publisher { get; set; } = null!;

    // public virtual ICollection<string> Discounts { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<ApplicationUser> Participants { get; set; }


    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is Event other
            && this.Id == other.Id
            && this.Title == other.Title
            && this.Description == other.Description
            && this.Price == other.Price
            && this.CreatedOn == other.CreatedOn
            && this.StartDateTime == other.StartDateTime
            && this.EndDateTime == other.EndDateTime
            && this.LocationName == other.LocationName
            && this.LocationUrl == other.LocationUrl
            && this.IsOnline == other.IsOnline)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
