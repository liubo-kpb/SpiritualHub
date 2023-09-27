namespace SpiritualHub.Client.ViewModels.Course;

public class CourseInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}
