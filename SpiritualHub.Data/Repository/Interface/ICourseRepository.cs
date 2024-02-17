namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface ICourseRepository : IDeletableRepository<Course>
{
    Task<Course?> GetCourseDetailsAsync(string id);

    Task<Course?> GetCourseInfoAsync(string id);

    Task<Course?> GetCourseWithStudentsAsync(string id);

    Task<Course?> GetCourseWithModulesImageAndRatingsAsync(string id);

    Task<Course?> GetCourseWithModulesByModuleIdAsync(string moduleId);

    Task<string?> GetCourseAuthorIdAsync(string id);

    Task<bool> CheckCourseActivityStatusAsync(string id);
}
