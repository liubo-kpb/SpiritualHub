namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Data.Models;
using Data.Repository.Interface;
using Interfaces;
using Mappings;
using Models.Course;
using Client.ViewModels.Course;
using Client.Infrastructure.Enums;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;

    private readonly ICourseRepository _courseRepository;

    public CourseService(
        ICourseRepository courseRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseViewModel>> AllCoursesByUserIdAsync(string userId)
    {
        var courses = await _courseRepository
            .AllAsNoTracking()
            .Include(c => c.Image)
            .Include(c => c.Author)
            .Include(c => c.Modules)
            .Where(c => c.Students.Any(s => s.Id.ToString() == userId))
            .ToListAsync();

        var coursesModel = new List<CourseViewModel>();
        _mapper.MapListToViewModel(courses, coursesModel);

        for (int i = 0; i < coursesModel.Count; i++)
        {
            coursesModel[i].HasCourse = true;
        }

        return coursesModel;
    }

    public Task<string> CreateAsync(CourseFormModel newCourse)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(CourseFormModel updatedCourse)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _courseRepository.AnyAsync(c => c.Id.ToString() == id);
    }

    public async Task<FilteredCoursesServiceModel> GetAllAsync(AllCoursesQueryModel queryModel, string userId)
    {
        var coursesQuery = _courseRepository
            .AllAsNoTracking()
            .Where(c => (bool) c.IsActive!);

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            coursesQuery = coursesQuery.Where(b => b.Category != null && b.Category!.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
            coursesQuery = coursesQuery.Where(b => EF.Functions.Like(b.Name, wildCard)
                                            || EF.Functions.Like(b.Author.Name, wildCard)
                                            || EF.Functions.Like(b.Description, wildCard)
                                            || EF.Functions.Like(b.ShortDescription, wildCard));
        }

        coursesQuery = queryModel.SortingOption switch
        {
            CourseSorting.Newest => coursesQuery.OrderByDescending(c => c.AddedOn),
            CourseSorting.Oldest => coursesQuery.OrderBy(c => c.AddedOn),
            CourseSorting.StudentsDescending=> coursesQuery.OrderByDescending(c => c.Students.Count),
            CourseSorting.StudentsAscending => coursesQuery.OrderBy(c => c.Students.Count),
            CourseSorting.PriceDescending => coursesQuery.OrderByDescending(c => c.Price),
            CourseSorting.PriceAscending => coursesQuery.OrderBy(c => c.Price),
            CourseSorting.TopRated => coursesQuery.OrderByDescending(c => c.Ratings.Count == 0 ? 0 : (c.Ratings.Sum(r => r.Stars) / (c.Ratings.Count * 1.0))),
            CourseSorting.LeastRated => coursesQuery.OrderBy(c => c.Ratings.Count == 0 ? 0 : (c.Ratings.Sum(r => r.Stars) / (c.Ratings.Count * 1.0))),
            _ => coursesQuery.OrderByDescending(c => c.AddedOn),
        };

        var courses = await coursesQuery
            .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
            .Take(queryModel.EntitiesPerPage)
            .Include(c => c.Image)
            .Include(c => c.Author)
            .Include(c => c.Modules)
            .Include(c => c.Students)
            .ToListAsync();

        var coursesModel = new List<CourseViewModel>();
        _mapper.MapListToViewModel(courses, coursesModel);

        for (int i = 0; i < coursesModel.Count; i++)
        {
            coursesModel[i].HasCourse = HasCourse(userId, courses[i]);
        }

        return new FilteredCoursesServiceModel()
        {
            Courses = coursesModel,
            TotalCoursesCount = coursesQuery.Count(),
        };
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _courseRepository
            .AllAsNoTracking()
            .CountAsync();
    }

    public Task GetAsync(string courseId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAuthorIdAsync(string courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<CourseDetailsViewModel> GetCourseDetailsAsync(string id, string userId)
    {
        var course = await _courseRepository.GetCourseDetailsAsync(id);
        
        var courseModel = _mapper.Map<CourseDetailsViewModel>(course!);
        // courseModel.Modules = courseModel.Modules.OrderBy(m => m.Number);
        courseModel.HasCourse = HasCourse(userId, course);

        return courseModel;
    }

    public Task<CourseFormModel> GetCourseInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CourseViewModel>> GetCoursesByPublisherIdAsync(string publisherId, string userId)
    {
        var courses = await _courseRepository
            .AllAsNoTracking()
            .Include(c => c.Image)
            .Include(c => c.Author)
            .Include(c => c.Modules)
            .Where(c => c.PublisherID.ToString() == publisherId)
            .ToListAsync();

        var coursesModel = new List<CourseViewModel>();
        _mapper.MapListToViewModel(courses, coursesModel);

        for (int i = 0; i < coursesModel.Count; i++)
        {
            coursesModel[i].HasCourse = HasCourse(userId, courses[i]);
        }

        return coursesModel;
    }

    public Task<bool> HasCourseAsync(string courseId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task HideAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string courseId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task ShowAsync(string id)
    {
        throw new NotImplementedException();
    }

    private static bool HasCourse(string userId, Course? course)
    {
        if (course!.Students.Any(p => p.Id.ToString() == userId))
        {
            return true;
        }

        return false;
    }
}
