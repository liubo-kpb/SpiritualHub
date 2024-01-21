namespace SpiritualHub.Data.Repository.Interface;

using Models;

public interface ICourseRepository : IDeletableRepository<Course>
{
    Task<Course?> GetCourseDetailsAsync(string id);

    Task<Course?> GetCourseInfoAsync(string id);

    Task<Course?> GetCourseWithStudents(string id);
}
