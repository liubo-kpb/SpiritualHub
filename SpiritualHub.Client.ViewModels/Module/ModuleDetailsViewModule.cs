namespace SpiritualHub.Client.ViewModels.Module;

public class ModuleDetailsViewModule : ModuleInfoViewModel
{
    public ModuleDetailsViewModule()
    {
        Modules = new HashSet<ModuleInfoViewModel>();
    }

    public string VideoUrl { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string NextModuleId { get; set; } = null!;

    public string PreviousModuleId { get; set; } = null!;

    public string CourseId { get; set; } = null!;

    public string AuthorId { get; set; } = null!;

    public ICollection<ModuleInfoViewModel> Modules { get; set; }

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is ModuleDetailsViewModule other
            && this.Id == other.Id
            && this.Number == other.Number
            && this.Name == other.Name
            && this.Description == other.Description
            && this.VideoUrl == other.VideoUrl
            && this.Text == other.Text
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
