namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Module;
using Data.Models;

public interface IModuleService
{
    Task<IList<CourseModuleViewModel>> GetModulesByCourseId(string courseId);

    /// <summary>
    /// Base edit of Module Name and Number properties.
    /// </summary>
    /// <param name="moduleEntity"></param>
    /// <param name="updatedModule"></param>
    void                                Edit(Module moduleEntity, CourseModuleViewModel updatedModule);

    Module                              Create(CourseModuleViewModel newModule, Guid courseId);

}
