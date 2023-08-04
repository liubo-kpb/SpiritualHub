namespace SpiritualHub.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Subscription
{
    public Subscription()
    {
        this.Id = Guid.NewGuid();
        this.Subscribers = new HashSet<ApplicationUser>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 5)")]
    public decimal Price { get; set; }

    [Required]
    public int SubscriptionTypeID { get; set; }

    public virtual Subscription SubscriptionType { get; set; }

    [Required]
    public Guid AuthorID { get; set; }

    public virtual Author Author { get; set; }

    public virtual ICollection<ApplicationUser> Subscribers { get; set; }
}
