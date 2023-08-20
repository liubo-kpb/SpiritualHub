namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Interfaces;
using Mappings;
using Data.Models;
using Data.Repository.Interface;
using Client.ViewModels.Category;

public class CategoryService : ICategoryService
{
    public readonly IRepository<Category> _categoryRepository;
    public readonly IMapper _mapper;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _categoryRepository.AnyAsync(c => c.Id == id);
    }

    public async Task<ICollection<CategoryServiceModel>> GetAllAsync()
    {
        var categoryEntities = await _categoryRepository
            .AllAsNoTracking()
            .ToListAsync();

        var categoryModels = new List<CategoryServiceModel>();

        GeneralMapping.MapListToViewModel(_mapper, categoryEntities, categoryModels);

        return categoryModels;
    }
}
