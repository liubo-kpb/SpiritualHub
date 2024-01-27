namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Module;
using Data.Models;

public interface IModuleService
{
    Task<IList<CourseModuleFormModel>> GetModulesByCourseId(string courseId);

    /// <summary>
    /// Base edit of Module Name and Number properties.
    /// </summary>
    /// <param name="moduleEntity"></param>
    /// <param name="updatedModule"></param>
    void                                Edit(Module moduleEntity, CourseModuleFormModel updatedModule);

    void                                ReorderCourseModules(ICollection<Module> modules);

    ICollection<Module>                 DeleteModules(ICollection<Module> moduleEntities, IEnumerable<CourseModuleFormModel> deletedModules);

    Task<Module>                        CreateModuleAsync(CourseModuleFormModel newModule);
}
