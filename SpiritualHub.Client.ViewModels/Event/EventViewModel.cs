namespace SpiritualHub.Client.ViewModels.Event;

using Author;
using BaseModels;

public class EventViewModel : BaseDetailsViewModel
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsOnline { get; set; }

    public bool IsUserJoined { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string LocationName { get; set; } = null!;

    public string LocationUrl { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string Participation
    {
        get
        {
            if (!string.IsNullOrEmpty(this.LocationName) && this.IsOnline)
            {
                return "In Person and Online";
            }
            else if (this.IsOnline)
            {
                return "Online only";
            }
            else
            {
                return "In Person only";
            }
        }
    }

    public AuthorInfoViewModel Author { get; set; } = null!;
}
