namespace SpiritualHub.Services;

using Microsoft.EntityFrameworkCore;

using Data.Models;
using Data.Repository.Interface;
using Services.Interfaces;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> _courseRepository;

    public CourseService(IRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _courseRepository
            .AllAsNoTracking()
            .CountAsync();
    }
}
