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

    public Module Create(CourseModuleViewModel newModule, Guid courseId)
    {
        return new Module()
        {
            Number = newModule.Number,
            Name = newModule.Name,
            CourseID = courseId,
        };
    }

    public void Edit(Module moduleEntity, CourseModuleViewModel updatedModule)
    {
        moduleEntity.Name = updatedModule.Name;
        moduleEntity.Number = updatedModule.Number;
    }

    public async Task<IList<CourseModuleViewModel>> GetModulesByCourseId(string courseId)
    {
        return await _moduleRepository
                            .AllAsNoTracking()
                            .Where(m => m.CourseID.ToString() == courseId)
                            .ProjectTo<CourseModuleViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
    }
}
