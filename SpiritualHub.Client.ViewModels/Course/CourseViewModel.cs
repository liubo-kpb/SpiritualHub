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

    public override bool Equals(object? obj)
    {
        if (base.Equals(obj))
        {
            return true;
        }
        else if (obj is CourseViewModel other
            && this.Id == other.Id
            && this.Name == other.Name
            && this.ShortDescription == other.ShortDescription
            && this.Price == other.Price
            && this.CategoryName == other.CategoryName
            && this.ModulesCount == other.ModulesCount
            && this.ImageUrl == other.ImageUrl
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
