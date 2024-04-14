namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Category;

public interface ICategoryService
{
    Task<ICollection<CategoryServiceModel>> GetAllAsync(string searchWord = null!);

    Task<CategoryServiceModel?>             GetSingleAsync(int id);

    Task<bool>                              ExistsAsync(int id);

    Task                                    AddAsync(string name);

    Task                                    EditAsync(int id, string name);

    Task                                    DeleteAsync(string id);
}
