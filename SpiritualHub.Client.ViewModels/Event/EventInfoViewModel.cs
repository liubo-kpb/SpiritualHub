namespace SpiritualHub.Client.ViewModels.Event;

using SpiritualHub.Client.ViewModels.Publisher;

public class EventInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public decimal Price { get; set; }

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is EventInfoViewModel other)
        {
            if (this.Id == other.Id
                && this.Title == other.Title
                && this.StartDateTime == other.StartDateTime
                && this.EndDateTime == other.EndDateTime
                && this.Price == other.Price)
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
