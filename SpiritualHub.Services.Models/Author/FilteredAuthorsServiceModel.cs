namespace SpiritualHub.Services.Models.Author;

using Client.ViewModels.Author;

public class FilteredAuthorsServiceModel
{
    public FilteredAuthorsServiceModel()
    {
        this.Authors = new HashSet<AuthorViewModel>();
    }

    public int TotalAuthorsCount { get; set; }

    public IEnumerable<AuthorViewModel> Authors { get; set; }
}
