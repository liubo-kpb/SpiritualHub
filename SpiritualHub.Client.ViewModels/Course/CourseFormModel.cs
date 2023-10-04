namespace SpiritualHub.Client.ViewModels.Course;

using System.ComponentModel.DataAnnotations;

using BaseModels;
using Module;

using static Common.EntityValidationConstants.Course;

public class CourseFormModel : BaseFormModel
{
    public CourseFormModel()
        : base()
    {
        this.Modules = new HashSet<ModuleInfoViewModel>();
    }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    [Display(Name = "Course Name")]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(DescriptionMinLength)]
    [Display(Name = "Full Course Description")]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    [Display(Name = "Short Description")]
    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; } = null!;

    [Display(Name = "Activate Course")]
    public bool IsActive { get; set; }

    public IEnumerable<ModuleInfoViewModel> Modules { get; set; }
}
