namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;

public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> _userRepository;

    public UserService(IRepository<ApplicationUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _userRepository.AllAsNoTracking().CountAsync();
    }
}
