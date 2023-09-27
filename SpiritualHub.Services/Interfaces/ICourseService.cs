namespace SpiritualHub.Services.Interfaces;

using Models.Course;
using Client.ViewModels.Course;

public interface ICourseService
{
    Task<int>                           GetAllCountAsync();

    Task<CourseDetailsViewModel>        GetCourseDetailsAsync(string id, string userId);

    Task<CourseFormModel>               GetCourseInfoAsync(string id);

    Task<bool>                          ExistsAsync(string id);

    Task<FilteredCoursesServiceModel>   GetAllAsync(AllCoursesQueryModel queryModel, string userId);

    Task<IEnumerable<CourseViewModel>>  AllCoursesByUserIdAsync(string userId);

    Task<IEnumerable<CourseViewModel>>  GetCoursesByPublisherIdAsync(string publisherId, string userId);

    Task<string>                        GetAuthorIdAsync(string courseId);

    Task<string>                        CreateAsync(CourseFormModel newCourse);

    Task                                EditAsync(CourseFormModel updatedCourse);

    Task                                DeleteAsync(string id);

    Task                                HideAsync(string id);

    Task                                ShowAsync(string id);

    Task<bool>                          HasCourseAsync(string courseId, string userId);

    Task                                GetAsync(string courseId, string userId);

    Task                                RemoveAsync(string courseId, string userId);
}
