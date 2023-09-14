namespace SpiritualHub.Client.ViewModels.Book;

using Publisher;

public class BookDetailsViewModel : BookViewModel
{
    public string Description { get; set; } = null!;

    public PublisherInfoViewModel Publisher { get; set; } = null!;
}
