namespace SpiritualHub.Client.ViewModels.Course;

using System.ComponentModel.DataAnnotations;

using Infrastructure.Enums;

using static Common.GeneralApplicationConstants;

public class AllCoursesQueryModel
{
    public AllCoursesQueryModel()
    {
        this.Categories = new HashSet<string>();
        this.Courses = new HashSet<CourseViewModel>();

        CurrentPage = DefaultPage;
        CoursesPerPage = EntitiesPerPageConstant;
    }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Search by text")]
    public string SearchTerm { get; set; } = null!;

    [Display(Name = "Sort by")]
    public CourseSorting SortingOption { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Show on Page")]
    public int CoursesPerPage { get; set; }

    public int TotalCoursesCount { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<CourseViewModel> Courses { get; set; }
}
