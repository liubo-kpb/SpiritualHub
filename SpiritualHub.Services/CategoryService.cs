﻿namespace SpiritualHub.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Interfaces;
using Mappings;
using Data.Models;
using Data.Repository.Interfaces;
using Client.ViewModels.Category;

public class CategoryService : ICategoryService
{
    public readonly IDeletableRepository<Category> _categoryRepository;
    public readonly IMapper _mapper;

    public CategoryService(IDeletableRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || await _categoryRepository.AnyAsync(c => c.Name == name))
        {
            return;
        }

        Category category = new Category()
        {
            Name = name,
        };

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var category = await _categoryRepository.GetSingleByIdAsync(id);

        _categoryRepository.Delete(category!);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task EditAsync(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || await _categoryRepository.AnyAsync(c => c.Name == name))
        {
            return;
        }

        var category = await _categoryRepository.GetSingleByIdAsync(id.ToString());

        category!.Name = name;

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _categoryRepository.AnyAsync(c => c.Id == id);
    }

    public async Task<ICollection<CategoryServiceModel>> GetAllAsync(string searchWord = null!)
    {
        var query = _categoryRepository
            .GetAll();

        if (!string.IsNullOrEmpty(searchWord))
        {
            query = query.Where(c => c.Name.ToLower().Contains(searchWord.ToLower()));
        }

        var categoryEntities = await query.ToListAsync();
        var categoryModels = new List<CategoryServiceModel>();

        _mapper.MapListToViewModel(categoryEntities, categoryModels);

        return categoryModels;
    }

    public async Task<CategoryServiceModel?> GetSingleAsync(int id)
    {
        return await _categoryRepository
                        .GetAll()
                        .ProjectTo<CategoryServiceModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(c => c.Id == id);
    }
}
