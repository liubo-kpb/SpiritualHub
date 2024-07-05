namespace SpiritualHub.Client.ViewModels.BaseModels;

public class BaseDetailsViewModel
{
    public string Id { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        return (obj is BaseDetailsViewModel other && this.Id == other.Id)  || base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
