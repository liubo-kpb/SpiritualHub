namespace SpiritualHub.Client.ViewModels.User;

using SpiritualHub.Client.ViewModels.Publisher;

public class UserServiceModel
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        bool result = false;

        if (base.Equals(obj))
        {
            result = true;
        }
        else if (obj is UserServiceModel other)
        {
            if (this.Id == other.Id
                && this.FullName == other.FullName
                && this.Email == other.Email
                && this.PhoneNumber == other.PhoneNumber)
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
