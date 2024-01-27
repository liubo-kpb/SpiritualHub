namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper.QueryableExtensions;
using AutoMapper;

using Interfaces;
using Client.ViewModels.Module;
using Data.Repository.Interface;
using Data.Models;

public class ModuleService : IModuleService
{
    private readonly IDeletableRepository<Module> _moduleRepository;
    private readonly IMapper _mapper;

    public ModuleService(
        IDeletableRepository<Module> moduleRepository,
        IMapper mapper)
    {
        _moduleRepository = moduleRepository;
        _mapper = mapper;
    }

    public void Edit(Module moduleEntity, CourseModuleFormModel updatedModule)
    {
        moduleEntity.Name = updatedModule.Name;
        moduleEntity.Number = updatedModule.Number;
    }

    public async Task<IList<CourseModuleFormModel>> GetModulesByCourseId(string courseId)
    {
        return await _moduleRepository
                            .AllAsNoTracking()
                            .Where(m => m.CourseID.ToString() == courseId)
                            .ProjectTo<CourseModuleFormModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
    }

    public async Task<Module> CreateModuleAsync(CourseModuleFormModel newModule)
    {
        var module = _mapper.Map<Module>(newModule);
        await _moduleRepository.AddAsync(module);

        return module;
    }

    public void ReorderCourseModules(ICollection<Module> modules)
    {
        var sortedModules = modules.OrderBy(m => m.Number).ThenBy(m => m.Name).ToList();

        int currentNumber = 1;
        foreach (var module in sortedModules)
        {
            module.Number = currentNumber++;
        }

        modules = sortedModules;
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

    public Task<int> GetAllCountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ModuleDetailsViewModule> GetCourseDetailsAsync(string id, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ModuleFormModel> GetCourseInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ModuleInfoViewModel>> AllCoursesByCourseIdAsync(string courseId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAuthorIdAsync(string moduleId)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateAsync(ModuleFormModel newCourse)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(ModuleFormModel updatedCourse)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task HideAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task ShowAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsActiveAsync(string moduleId)
    {
        throw new NotImplementedException();
    }
}
