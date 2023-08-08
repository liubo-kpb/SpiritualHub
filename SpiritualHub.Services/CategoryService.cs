namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Client.ViewModels.Category;
using Data.Models;
using Data.Repository.Interface;
using Services.Interfaces;

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
        var categoryEntities = await _categoryRepository.AllAsNoTrackingAsync();
        var categoryModels = new List<CategoryServiceModel>();

        foreach (var category in categoryEntities)
        {
            var model = _mapper.Map<CategoryServiceModel>(category);
            categoryModels.Add(model);
        }

        return categoryModels;
    }
}
