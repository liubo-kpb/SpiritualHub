namespace SpiritualHub.Services.Interfaces;

using Data.Models;
using Client.ViewModels.Module;

public interface IModuleService
{
    Task<IList<CourseModuleFormModel>> GetModulesByCourseId(string courseId);

    /// <summary>
    /// Base edit of Module Name and Number properties.
    /// </summary>
    /// <param name="moduleEntity"></param>
    /// <param name="updatedModule"></param>
    void                                    Edit(Module moduleEntity, CourseModuleFormModel updatedModule);

    void                                    ReorderCourseModules(ICollection<Module> modules);

    ICollection<Module>                     DeleteModules(ICollection<Module> moduleEntities, IEnumerable<CourseModuleFormModel> deletedModules);

    Task<Module>                            CreateModuleAsync(CourseModuleFormModel newModule);

    Task<int>                               GetAllCountAsync();

    Task<ModuleDetailsViewModule>           GetCourseDetailsAsync(string id, string userId);

    Task<ModuleFormModel>                   GetCourseInfoAsync(string id);

    Task<bool>                              ExistsAsync(string id);

    Task<IEnumerable<ModuleInfoViewModel>>  AllCoursesByCourseIdAsync(string courseId);

    Task<string>                            GetAuthorIdAsync(string moduleId);

    Task<string>                            CreateAsync(ModuleFormModel newCourse);

    Task                                    EditAsync(ModuleFormModel updatedCourse);

    Task                                    DeleteAsync(string id);

    Task                                    HideAsync(string id);

    Task                                    ShowAsync(string id);

    Task<bool>                              IsActiveAsync(string moduleId);
}
