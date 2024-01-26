namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface ICourseRepository : IDeletableRepository<Course>
{
    Task<Course?> GetCourseDetailsAsync(string id);

    Task<Course?> GetCourseInfoAsync(string id);

    Task<Course?> GetCourseWithStudentsAsync(string id);

    Task<Course?> GetCourseWithModulesAsync(string id);

    Task<string?> GetCourseAuthorIdAsync(string id);
}
