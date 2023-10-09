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
    private readonly IRepository<Module> _moduleRepository;
    private readonly IMapper _mapper;

    public ModuleService(IRepository<Module> moduleRepository,
        IMapper mapper)
    {
        _moduleRepository = moduleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModuleInfoViewModel>> GetModulesByCourseId(string courseId)
    {
        return await _moduleRepository
                            .AllAsNoTracking()
                            .Where(m => m.CourseID.ToString() == courseId)
                            .ProjectTo<ModuleInfoViewModel>(_mapper.ConfigurationProvider)
                            .ToListAsync();
    }
}
