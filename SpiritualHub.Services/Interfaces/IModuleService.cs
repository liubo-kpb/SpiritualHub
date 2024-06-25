namespace SpiritualHub.Services.Interfaces;

using Data.Models;
using Client.ViewModels.Module;
using SpiritualHub.Client.ViewModels.Module.Interfaces;

public interface IModuleService
{
    /// <summary>
    /// Base edit of Module Name and Number properties.
    /// </summary>
    /// <param name="moduleEntity"></param>
    /// <param name="updatedModule"></param>
    void                                    Edit(Module moduleEntity, IModuleBaseFormModel updatedModule);

    /// <summary>
    /// Reorder modules when there is a change in the module numbering of a course. 
    /// </summary>
    /// <param name="modules"></param>
    /// <param name="skipNumber">Intended for when there is a module placed in the middle of the list by its number.</param>
    void                                    ReorderCourseModules(IEnumerable<Module> modules, int skipNumber = int.MinValue);

    Task                                    AdjustModulesNumberingAsync(ModuleFormModel moduleForm, bool isNew = false);

    ICollection<Module>                     DeleteMultiple(ICollection<Module> moduleEntities, IEnumerable<CourseModuleFormModel> deletedModules);

    Task<int>                               GetAllCountAsync();

    Task<ModuleDetailsViewModule>           GetModuleDetailsAsync(string id);

    string?                                 GetNextModuleId(ModuleDetailsViewModule moduleViewModel, bool canAccess);

    string?                                 GetPreviousModuleId(ModuleDetailsViewModule moduleViewModel, bool canAccess);

    Task<ModuleFormModel>                   GetModuleInfoAsync(string id);

    Task<bool>                              ExistsAsync(string id);

    Task<string>                            GetAuthorIdAsync(string moduleId);

    Task<string>                            GetCourseIdAsync(string moduleId);

    Task<string>                            CreateAsync(ModuleFormModel newModule);

    Task                                    EditAsync(ModuleFormModel updatedModule);

    Task                                    DeleteAsync(string id);

    Task                                    HideAsync(string id);

    Task                                    ShowAsync(string id);

    Task<bool>                              IsActiveAsync(string moduleId);
}
