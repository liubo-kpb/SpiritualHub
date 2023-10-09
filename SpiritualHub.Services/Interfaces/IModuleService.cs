namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Module;

public interface IModuleService
{
    Task<IEnumerable<ModuleInfoViewModel>> GetModulesByCourseId(string courseId);
}
