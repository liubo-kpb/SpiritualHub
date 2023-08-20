namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Category;

public interface ICategoryService
{
    Task<ICollection<CategoryServiceModel>> GetAllAsync();

    Task<bool> ExistsAsync(int id);
}
