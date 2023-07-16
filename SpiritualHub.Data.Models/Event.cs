namespace SpiritualHub.Data.Models;

public class Event
{
    public Event()
    {
        this.Id = Guid.NewGuid();
        this.Discounts = new HashSet<string>();
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime StartingDateTime { get; set;}

    public DateTime EndingDateTime { get; set;}

    public bool IsOnline { get; set; }

    public Guid AutorID { get; set; }

    public virtual Author Author { get; set; }

    public Guid OrganizerID { get; set; }

    public virtual Publisher Publisher { get; set; }

    public int CategoryID { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<string> Discounts { get; set; }

    public virtual ICollection<Event> Participants { get; set; }
}
