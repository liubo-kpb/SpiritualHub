namespace SpiritualHub.Services.Interfaces;

public interface IApplicationUserService
{
    Task<int> GetAllCountAsync();

    Task<string> GetUserFullName(string userId);
}
