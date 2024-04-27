namespace SpiritualHub.Client.ViewModels.Course;

using SpiritualHub.Client.ViewModels.Publisher;

public class CourseInfoViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is CourseInfoViewModel other)
        {
            if (this.Id == other.Id
                && this.Name == other.Name
                && this.ShortDescription == other.ShortDescription
                && this.Price == other.Price
                && this.IsActive == other.IsActive)
            {
                result = true;
            }
        }

        return result;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
