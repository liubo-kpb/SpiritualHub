namespace SpiritualHub.Client.ViewModels.Author;

public class AllAuthorsQueryModel
{
    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<AuthorViewModel> Authors { get; set; }
}