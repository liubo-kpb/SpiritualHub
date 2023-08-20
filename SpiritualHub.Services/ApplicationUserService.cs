namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;

public class ApplicationUserService : IApplicationUserService
{
    private readonly IRepository<ApplicationUser> _userRepository;

    public ApplicationUserService(IRepository<ApplicationUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _userRepository.AllAsNoTracking().CountAsync();
    }

    public async Task<string> GetUserFullName(string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));

        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
        {
            return null;
        }

        return user!.FirstName + " " + user!.LastName;
    }
}
