﻿namespace SpiritualHub.Client.ViewModels.Course;

using System.ComponentModel.DataAnnotations;

using BaseModels;
using Module;

using static Common.EntityValidationConstants.Course;

public class CourseFormModel : BaseFormModel
{
    public CourseFormModel()
    {
        this.Modules = new List<CourseModuleFormModel>();
    }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    [Display(Name = "Course Name")]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(DescriptionMinLength)]
    [Display(Name = "Full Description")]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(ShortDescriptionMaxLength, MinimumLength = ShortDescriptionMinLength)]
    [Display(Name = "Short Description")]
    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; } = null!;

    [Display(Name = "Publish Course?")]
    public bool IsActive { get; set; }

    public IList<CourseModuleFormModel> Modules { get; set; }
}
