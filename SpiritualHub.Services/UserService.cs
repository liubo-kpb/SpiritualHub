namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Interfaces;
using Data.Models;
using Data.Repository.Interface;
using Client.ViewModels.User;

public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IMapper _mapper;

    public UserService(
        IRepository<ApplicationUser> userRepository,
        IRepository<Publisher> publisherRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserServiceModel>> GetAllAsync()
    {
        var allUsers = new List<UserServiceModel>();

        var publishers = await _publisherRepository
            .AllAsNoTracking()
            .Include(p => p.User)
            .ProjectTo<UserServiceModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        allUsers.AddRange(publishers);

        var users = await _userRepository
            .AllAsNoTracking()
            .Where(u => !_publisherRepository.AllAsNoTracking().Any(p => p.UserID == u.Id))
            .ProjectTo<UserServiceModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

        allUsers.AddRange(users);

        return allUsers;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _userRepository
                        .AllAsNoTracking()
                        .CountAsync();
    }

    public async Task<string?> GetUserFullName(string userId)
    {
        var user = await _userRepository.GetSingleByIdAsync(Guid.Parse(userId));

        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
        {
            return null;
        }

        return user!.FirstName + " " + user!.LastName;
    }
}
