namespace SpiritualHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SpiritualHub.Common.EntityValidationConstants.Event;

public class Event
{
    public Event()
    {
        this.Id             = Guid.NewGuid();
        // this.Discounts      = new HashSet<string>();
        this.Participants   = new HashSet<ApplicationUser>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; }

    [Required]
    [MinLength(DescriptionMinLength)]
    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime StartDateTime { get; set;}

    [Required]
    public DateTime EndDateTime { get; set;}

    [Required]
    public bool IsOnline { get; set; }

    [Required]
    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    [Required]
    public Guid AutorID { get; set; }

    public virtual Author Author { get; set; }
    
    public Guid OrganizerID { get; set; }

    public virtual Publisher Organizer { get; set; } = null!;

    // public virtual ICollection<string> Discounts { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; }

    public virtual ICollection<ApplicationUser> Participants { get; set; }
}
