namespace SpiritualHub.Client.ViewModels.Module;

public class ModuleDetailsViewModule : ModuleInfoViewModel
{
    public string VideoUrl { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string NextModuleId { get; set; } = null!;

    public string PreviousModuleId { get; set; } = null!;

    public string CourseId { get; set; } = null!;
}
