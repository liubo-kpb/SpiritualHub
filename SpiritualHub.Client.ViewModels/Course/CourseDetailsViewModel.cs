namespace SpiritualHub.Client.ViewModels.Course;

using Publisher;

public class CourseDetailsViewModel : CourseViewModel
{
    public DateTime AddedOn { get; set; }

    public PublisherInfoViewModel Publisher { get; set; } = null!;
}
