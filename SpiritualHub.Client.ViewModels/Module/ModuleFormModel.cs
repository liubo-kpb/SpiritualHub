namespace SpiritualHub.Client.ViewModels.Module;

using System.ComponentModel.DataAnnotations;

using BaseModels;
using Course;

using static Common.EntityValidationConstants.Module;


public class ModuleFormModel : BaseFormModel
{
    public ModuleFormModel()
        : base()
    {
        this.Courses = new HashSet<CourseInfoViewModel>();
    }

    [Display(Name = "Module Order Number")]
    public int Number { get; set; }

    [Required]
    [Display(Name = "Module Name")]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Module Description")]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Display(Name = "Video URL (YouTube or Vimeo)")]
    public string? VideoUrl { get; set; } = null!;

    [Display(Name = "Module Text")]
    public string? Text { get; set; } = null!;

    [Display(Name = "Publish Module?")]
    public bool IsActive { get; set; }

    [Required]
    [Display(Name = "Choose Course:")]
    public string CourseId { get; set; } = null!;

    public IEnumerable<CourseInfoViewModel> Courses { get; set; }
}
