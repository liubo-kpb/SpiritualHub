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

    public ModuleService(IDeletableRepository<Module> moduleRepository,
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
}
