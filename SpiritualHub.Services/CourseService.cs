namespace SpiritualHub.Services;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Data.Models;
using Data.Repository.Interfaces;
using Interfaces;
using Mappings;
using Models.Course;
using Client.ViewModels.Course;
using Client.Infrastructure.Enums;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IModuleService _moduleService;

    private readonly IDeletableRepository<Image> _imageRepository;
    private readonly IDeletableRepository<Rating> _ratingRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        IModuleService moduleService,
        IDeletableRepository<Image> imageRepository,
        IDeletableRepository<Rating> ratingRepository,
        IRepository<ApplicationUser> userRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _moduleService = moduleService;
        _imageRepository = imageRepository;
        _ratingRepository = ratingRepository;
        _userRepository = userRepository;
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
            coursesModel[i].UserHasCourse = true;
        }

        return coursesModel;
    }

    public async Task<string> CreateAsync(CourseFormModel newCourse)
    {
        var courseEntity = _mapper.Map<Course>(newCourse);
        courseEntity.Image.Name = courseEntity.Name;

        courseEntity.Modules = _moduleService.ReorderCourseModules(courseEntity.Modules);

        await _courseRepository.AddAsync(courseEntity);
        await _courseRepository.SaveChangesAsync();

        return courseEntity.Id.ToString();
    }

    public async Task DeleteAsync(string id)
    {
        var course = await _courseRepository.GetCourseWithModulesImageAndRatingsAsync(id);

        // Modules are Cascade deleted.
        _courseRepository.Delete(course!);
        _imageRepository.Delete(course!.Image);
        _ratingRepository.DeleteMultiple(course!.Ratings);

        await _courseRepository.SaveChangesAsync();
    }

    public async Task EditAsync(CourseFormModel updatedCourse)
    {
        var course = await _courseRepository.GetCourseInfoAsync(updatedCourse.Id!);

        course!.Name = updatedCourse.Name;
        course.ShortDescription = updatedCourse.ShortDescription;
        course.Description = updatedCourse.Description;
        course.Price = updatedCourse.Price;
        course.Image.URL = updatedCourse.ImageUrl;
        course.IsActive = updatedCourse.IsActive;
        course.AuthorID = Guid.Parse(updatedCourse.AuthorId!);
        course.CategoryID = updatedCourse.CategoryId;
        course.PublisherID = Guid.Parse(updatedCourse.PublisherId!);

        var deletedModules = updatedCourse.Modules.Where(m => m.IsDeleted);
        if (deletedModules.Any())
        {
            var removedModules = _moduleService.DeleteModules(course.Modules, deletedModules);

            updatedCourse.Modules = updatedCourse.Modules.Except(deletedModules).ToList();
            course.Modules = course.Modules.Except(removedModules).ToList();
        }

        var newModules = updatedCourse.Modules.Where(m => m.IsNew);
        foreach (var newModule in newModules)
        {
            var module = _mapper.Map<Module>(newModule);
            course.Modules.Add(module);
        }

        foreach (var updatedModule in updatedCourse.Modules.Where(m => m.Id != null))
        {
            var moduleEntity = course.Modules.FirstOrDefault(m => m.Id.ToString() == updatedModule.Id);
            _moduleService.Edit(moduleEntity!, updatedModule);
        }

        course.Modules = _moduleService.ReorderCourseModules(course.Modules);

        await _courseRepository.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _courseRepository.AnyAsync(c => c.Id.ToString() == id);
    }

    public async Task<FilteredCoursesServiceModel> GetAllAsync(AllCoursesQueryModel queryModel, string userId)
    {
        var coursesQuery = _courseRepository
                                .AllAsNoTracking()
                                .Where(c => c.IsActive);

        int courseCount = coursesQuery.Count();

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            coursesQuery = coursesQuery.Where(c => c.Category != null && c.Category!.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
            coursesQuery = coursesQuery.Where(c => c.Name.ToLower().Contains(wildCard)
                                                || c.Author.Name.ToLower().Contains(wildCard)
                                                || c.Author.Alias.ToLower().Contains(wildCard)
                                                || c.Description.ToLower().Contains(wildCard)
                                                || c.ShortDescription.ToLower().Contains(wildCard));
        }

        coursesQuery = queryModel.SortingOption switch
        {
            CourseSorting.Newest => coursesQuery.OrderByDescending(c => c.AddedOn),
            CourseSorting.Oldest => coursesQuery.OrderBy(c => c.AddedOn),
            CourseSorting.StudentsDescending => coursesQuery.OrderByDescending(c => c.Students.Count),
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
            coursesModel[i].UserHasCourse = HasCourse(userId, courses[i]);
        }

        return new FilteredCoursesServiceModel()
        {
            Courses = coursesModel,
            TotalCoursesCount = courseCount,
        };
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _courseRepository
                            .AllAsNoTracking()
                            .CountAsync();
    }

    public async Task GetAsync(string courseId, string userId)
    {
        var course = await _courseRepository.GetSingleByIdAsync(courseId);
        var user = await _userRepository.GetSingleByIdAsync(userId);
        
        course!.Students.Add(user!);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task<string> GetAuthorIdAsync(string courseId)
    {
        return (await _courseRepository.GetCourseAuthorIdAsync(courseId))!;
    }

    public async Task<CourseDetailsViewModel> GetCourseDetailsAsync(string id, string userId)
    {
        var course = await _courseRepository.GetCourseDetailsAsync(id);

        var courseModel = _mapper.Map<CourseDetailsViewModel>(course!);
        courseModel.Modules = courseModel.Modules.OrderBy(m => m.Number);
        courseModel.UserHasCourse = HasCourse(userId, course);

        return courseModel;
    }

    public async Task<CourseFormModel> GetCourseInfoAsync(string id)
    {
        var course = await _courseRepository.GetCourseDetailsAsync(id);

        var courseModel = _mapper.Map<CourseFormModel>(course!);
        courseModel.Modules = courseModel.Modules
                                            .OrderBy(m => m.Number)
                                            .ToList();

        return courseModel;
    }

    public async Task<IEnumerable<CourseViewModel>> GetCoursesByPublisherIdAsync(string publisherId, string userId)
    {
        var courses = await _courseRepository
            .AllAsNoTracking()
            .Include(c => c.Image)
            .Include(c => c.Author)
            .Include(c => c.Modules)
            .Include(c => c.Students)
            .Where(c => c.PublisherID.ToString() == publisherId)
            .ToListAsync();

        var coursesModel = new List<CourseViewModel>();
        _mapper.MapListToViewModel(courses, coursesModel);

        for (int i = 0; i < coursesModel.Count; i++)
        {
            coursesModel[i].UserHasCourse = HasCourse(userId, courses[i]);
        }

        return coursesModel;
    }

    public async Task<bool> HasCourseAsync(string courseId, string userId)
    {
        return await _courseRepository
            .AnyAsync(
            c => c.Id.ToString() == courseId && c.Students.Any(s => s.Id.ToString() == userId));
    }

    public async Task HideAsync(string id)
    {
        await ChangeCourseActivityStatusAsync(id, false);
    }


    public async Task RemoveAsync(string courseId, string userId)
    {
        var course = await _courseRepository.GetCourseWithStudentsAsync(courseId);
        var user = await _userRepository.GetSingleByIdAsync(userId);

        course!.Students.Remove(user!);
        await _userRepository.SaveChangesAsync();
    }

    public async Task ShowAsync(string id)
    {
        await ChangeCourseActivityStatusAsync(id, true);
    }

    public async Task<bool> IsActiveAsync(string courseId)
    {
        return await _courseRepository.CheckCourseActivityStatusAsync(courseId);
    }

    private static bool HasCourse(string userId, Course? course)
    {
        if (course!.Students.Any(s => s.Id.ToString() == userId))
        {
            return true;
        }

        return false;
    }

    private async Task ChangeCourseActivityStatusAsync(string id, bool newStatus)
    {
        var course = await _courseRepository.GetCourseWithModulesImageAndRatingsAsync(id);
        course!.IsActive = newStatus;

        foreach (var module in course.Modules)
        {
            module.IsActive = newStatus;
        }

        await _courseRepository.SaveChangesAsync();
    }
}