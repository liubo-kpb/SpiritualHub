﻿namespace SpiritualHub.Services;

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
    private readonly IModuleRepository _moduleRepository;
    private readonly IMapper _mapper;

    public ModuleService(
        IModuleRepository moduleRepository,
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

    public async Task<Module> CreateAsync(CourseModuleFormModel newModule)
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

    public Task<ModuleDetailsViewModule> GetModuleDetailsAsync(string id, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ModuleFormModel> GetModuleInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _moduleRepository.AnyAsync(m => m.Id.ToString() == id);
    }

    public Task<IEnumerable<ModuleInfoViewModel>> AllModulesByCourseIdAsync(string courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetAuthorIdAsync(string moduleId)
    {
        return (await _moduleRepository.GetAuthordId(moduleId))!;
    }

    public async Task<string> CreateAsync(ModuleFormModel newModule)
    {
        var newModuleEntity = _mapper.Map<Module>(newModule);
        
        ICollection<Module> courseModules = await _moduleRepository.GetAll().Where(m => m.CourseID == newModuleEntity.CourseID).ToListAsync();
        courseModules.Add(newModuleEntity);
        ReorderCourseModules(courseModules);

        await _moduleRepository.AddAsync(newModuleEntity);
        await _moduleRepository.SaveChangesAsync();

        return newModuleEntity.Id.ToString();
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
        return _moduleRepository.AnyAsync(m => m.Id.ToString() == moduleId && m.IsActive);
    }

    public async Task<string> GetCourseIdAsync(string moduleId)
    {
        return (await _moduleRepository.GetCourseIdByModuleId(moduleId))!;
    }
}
