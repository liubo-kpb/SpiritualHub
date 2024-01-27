namespace SpiritualHub.Client.ViewModels.Module;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.Module;

public class CourseModuleFormModel
{
    public string? Id { get; set; } = null!;

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    public int Number { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsNew {  get; set; }
}
