namespace SpiritualHub.Client.ViewModels.Author;

using SpiritualHub.Client.ViewModels.Publisher;

public class AuthorDetailsViewModel : AuthorViewModel
{
    public AuthorDetailsViewModel()
    {
        this.Publishers = new HashSet<PublisherInfoViewModel>();
    }

    public IEnumerable<PublisherInfoViewModel> Publishers { get; set; }
}
