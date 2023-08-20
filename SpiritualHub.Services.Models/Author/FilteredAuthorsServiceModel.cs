namespace SpiritualHub.Services.Models.Author;

using SpiritualHub.Client.ViewModels.Author;

public class FilteredAuthorsServiceModel
{
    public int TotalAuthorsCount { get; set; }

    public IEnumerable<AuthorViewModel> Authors { get; set; }
}
