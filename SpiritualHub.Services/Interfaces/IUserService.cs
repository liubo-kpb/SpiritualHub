namespace SpiritualHub.Services.Interfaces;

using SpiritualHub.Client.ViewModels.User;

public interface IUserService
{
    Task<int> GetAllCountAsync();

    Task<string?> GetUserFullName(string userId);

    Task<IEnumerable<UserServiceModel>> GetAllAsync();
}