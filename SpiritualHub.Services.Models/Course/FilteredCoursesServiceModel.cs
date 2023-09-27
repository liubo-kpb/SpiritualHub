namespace SpiritualHub.Services.Models.Course;

using Client.ViewModels.Course;

public class FilteredCoursesServiceModel
{
    public FilteredCoursesServiceModel()
    {
        this.Courses = new HashSet<CourseViewModel>();
    }

    public int TotalCoursesCount { get; set; }

    public IEnumerable<CourseViewModel> Courses { get; set; }
}
