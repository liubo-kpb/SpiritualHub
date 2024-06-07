namespace SpiritualHub.Client.ViewModels.Module;

using System.ComponentModel.DataAnnotations;

using BaseModels;
using Course;
using Interfaces;

using static Common.EntityValidationConstants.Module;


public class ModuleFormModel : BaseFormModel, IModuleBaseFormModel
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

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is ModuleFormModel other
            && this.Id == other.Id
            && this.Number == other.Number
            && this.Name == other.Name
            && this.Description == other.Description
            && this.VideoUrl == other.VideoUrl
            && this.Text == other.Text
            && this.IsActive == other.IsActive)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}