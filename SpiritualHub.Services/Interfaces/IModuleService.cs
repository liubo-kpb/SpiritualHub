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

    ICollection<Module>                     ReorderCourseModules(IEnumerable<Module> modules, int startingNumber = 1);

    Task                                    AdjustModulesNumbering(ModuleFormModel moduleForm, bool isNew = false);

    ICollection<Module>                     DeleteMultiple(ICollection<Module> moduleEntities, IEnumerable<CourseModuleFormModel> deletedModules);

    Task<int>                               GetAllCountAsync();

    Task<ModuleDetailsViewModule>           GetModuleDetailsAsync(string id, string userId);

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
