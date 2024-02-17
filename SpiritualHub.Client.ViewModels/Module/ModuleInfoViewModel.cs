namespace SpiritualHub.Client.ViewModels.Module;

using BaseModels;

public class ModuleInfoViewModel : BaseDetailsViewModel
{
    public string Name { get; set; } = null!;

    public int Number {  get; set; }

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }
}
