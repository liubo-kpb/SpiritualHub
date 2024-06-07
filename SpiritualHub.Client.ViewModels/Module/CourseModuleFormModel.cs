namespace SpiritualHub.Client.ViewModels.Module;

using System.ComponentModel.DataAnnotations;

using Interfaces;

using static Common.EntityValidationConstants.Module;

public class CourseModuleFormModel : IModuleBaseFormModel
{
    public string? Id { get; set; } = null!;

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    public int Number { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsNew {  get; set; }

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is CourseModuleFormModel other
            && this.Id == other.Id
            && this.Number == other.Number
            && this.Name == other.Name
            && this.IsNew == other.IsNew
            && this.IsDeleted == other.IsDeleted)
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
