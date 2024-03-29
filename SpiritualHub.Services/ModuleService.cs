namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Client.ViewModels.Module;
using Data.Repository.Interfaces;
using Data.Models;
using SpiritualHub.Services.Mappings;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public ModuleService(
        IModuleRepository moduleRepository,
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _moduleRepository = moduleRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public void Edit(Module moduleEntity, CourseModuleFormModel updatedModule)
    {
        moduleEntity.Name = updatedModule.Name;
        moduleEntity.Number = updatedModule.Number;
    }

    public async Task<Module> CreateAsync(CourseModuleFormModel newModule)
    {
        var module = _mapper.Map<Module>(newModule);
        await _moduleRepository.AddAsync(module);

        return module;
    }

    public void ReorderCourseModules(IEnumerable<Module> modules, int startingNumber = 1)
    {
        var sortedModules = modules.OrderBy(m => m.Number).ThenBy(m => m.Name).ToList();

        int currentNumber = startingNumber;
        foreach (var module in sortedModules)
        {
            module.Number = currentNumber++;
        }
    }

    public ICollection<Module> DeleteModules(ICollection<Module> moduleEntities, IEnumerable<CourseModuleFormModel> deletedModules)
    {
        ICollection<Module> modulesToDelete = new List<Module>();
        foreach (var module in deletedModules)
        {
            var moduleEntity = moduleEntities.FirstOrDefault(m => m.Id.ToString() == module.Id)!;
            modulesToDelete.Add(moduleEntity);
        }

        _moduleRepository.DeleteMultiple(modulesToDelete);

        return modulesToDelete;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _moduleRepository.AllAsNoTracking().CountAsync();
    }

    public async Task<ModuleDetailsViewModule> GetModuleDetailsAsync(string id, string userId)
    {

        var courseEntity = await _courseRepository.GetCourseWithModulesByModuleIdAsync(id);
        var modules = courseEntity!.Modules.OrderBy(m => m.Number);

        var moduleViewModule = _mapper.Map<ModuleDetailsViewModule>(modules.FirstOrDefault(m => m.Id.ToString() == id));
        _mapper.MapListToViewModel(modules, moduleViewModule.Modules);

        moduleViewModule.AuthorId = courseEntity!.AuthorID.ToString();

        return moduleViewModule;
    }

    public string? GetNextModuleId(ModuleDetailsViewModule moduleViewModel, bool canAccess)
    {
        var modules = moduleViewModel.Modules.Where(m => m.Number > moduleViewModel.Number);
        return GetModuleIdFromList(modules, canAccess);
    }

    public string? GetPreviousModuleId(ModuleDetailsViewModule moduleViewModel, bool canAccess)
    {
        var modules = moduleViewModel
                        .Modules
                        .Where(m => m.Number < moduleViewModel.Number)
                        .OrderByDescending(m => m.Number);

        return GetModuleIdFromList(modules, canAccess);
    }

    public async Task<ModuleFormModel> GetModuleInfoAsync(string id)
    {
        var module = await _moduleRepository.GetSingleByIdAsync(id);

        return _mapper.Map<ModuleFormModel>(module);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _moduleRepository.AnyAsync(m => m.Id.ToString() == id);
    }

    public async Task<string> GetAuthorIdAsync(string moduleId)
    {
        return (await _moduleRepository.GetAuthordId(moduleId))!;
    }

    public async Task<string> CreateAsync(ModuleFormModel newModule)
    {
        var newModuleEntity = _mapper.Map<Module>(newModule);

        await _moduleRepository.AddAsync(newModuleEntity);
        await _moduleRepository.SaveChangesAsync();

        return newModuleEntity.Id.ToString();
    }

    public async Task EditAsync(ModuleFormModel updatedModule)
    {
        var moduleEntity = await _moduleRepository.GetSingleByIdAsync(updatedModule.Id!);

        moduleEntity!.Number = updatedModule.Number;
        moduleEntity.Name = updatedModule.Name;
        moduleEntity.Description = updatedModule.Description;
        moduleEntity.VideoUrl = updatedModule.VideoUrl!;
        moduleEntity.Text = updatedModule.Text!;
        moduleEntity.IsActive = updatedModule.IsActive;
        moduleEntity.CourseID = Guid.Parse(updatedModule.CourseId);

        await _moduleRepository.SaveChangesAsync();
    }

    public async Task AdjustModulesNumbering(ModuleFormModel moduleForm, bool isNew = false)
    {
        var courseModules = _moduleRepository.GetModulesByCourseId(moduleForm.CourseId);

        if (moduleForm.Number > courseModules.Count())
        {
            if (isNew)
            {
                moduleForm.Number = courseModules.Count() + 1;
            }
            else
            {
                moduleForm.Number = courseModules.Count();
            }
        }
        else
        {
            ReorderCourseModules(courseModules.Where(m => m.Number >= moduleForm.Number && m.Id.ToString() != moduleForm.Id), moduleForm.Number + 1);
            await _moduleRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(string id)
    {
        var module = await _moduleRepository.GetSingleByIdAsync(id);

        _moduleRepository.Delete(module!);

        await _moduleRepository.SaveChangesAsync();
    }

    public async Task HideAsync(string id)
    {
        await ChangeModuleActivityStatus(id, false);
    }

    public async Task ShowAsync(string id)
    {
        await ChangeModuleActivityStatus(id, true);
    }

    public Task<bool> IsActiveAsync(string moduleId)
    {
        return _moduleRepository.AnyAsync(m => m.Id.ToString() == moduleId && m.IsActive);
    }

    public async Task<string> GetCourseIdAsync(string moduleId)
    {
        return (await _moduleRepository.GetCourseIdByModuleId(moduleId))!;
    }

    private async Task ChangeModuleActivityStatus(string id, bool newStatus)
    {
        var module = await _moduleRepository.GetSingleByIdAsync(id);
        module!.IsActive = newStatus;

        await _moduleRepository.SaveChangesAsync();
    }

    private static string? GetModuleIdFromList(IEnumerable<ModuleInfoViewModel> modules, bool canAccess)
    {
        if (modules.Any())
        {
            return modules.FirstOrDefault(m => m.IsActive || canAccess)?.Id ?? null!;
        }

        return null!;
    }
}
