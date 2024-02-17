namespace SpiritualHub.Client.ViewModels.Course;

using BaseModels;
using Author;

public class CourseViewModel : BaseDetailsViewModel
{
    public string Name { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public string CategoryName { get; set; } = null!;

    public int ModulesCount { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool UserHasCourse { get; set; }

    public AuthorInfoViewModel Author { get; set; } = null!;
}
