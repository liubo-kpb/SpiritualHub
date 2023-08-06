namespace SpiritualHub.Client.ViewModels.Author;

public class AuthorViewModel
{
    public Guid Id { get; set; }

    public string Alias { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;
}
