namespace SpiritualHub.Client.ViewModels.Course;

using Module;
using Publisher;

public class CourseDetailsViewModel : CourseViewModel
{
    public CourseDetailsViewModel()
    {
        this.Modules = new HashSet<ModuleInfoViewModel>();
    }

    public DateTime AddedOn { get; set; }

    public string Description { get; set; } = null!;

    public PublisherInfoViewModel Publisher { get; set; } = null!;

    public IEnumerable<ModuleInfoViewModel> Modules { get; set; }
}
