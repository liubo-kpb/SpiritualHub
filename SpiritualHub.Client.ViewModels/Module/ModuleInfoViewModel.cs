namespace SpiritualHub.Client.ViewModels.Module;

using BaseModels;

public class ModuleInfoViewModel : BaseDetailsViewModel
{
    public string Name { get; set; } = null!;

    public int Number {  get; set; }

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is ModuleInfoViewModel other
            && this.Id == other.Id
            && this.Name == other.Name
            && this.Number == other.Number
            && this.Description == other.Description
            && this.IsActive == other.IsActive)
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
