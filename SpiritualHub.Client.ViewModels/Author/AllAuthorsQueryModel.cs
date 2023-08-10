namespace SpiritualHub.Client.ViewModels.Author;

using System.ComponentModel.DataAnnotations;

using Infrastructure.Enums;

using static Common.GeneralApplicationConstants;

public class AllAuthorsQueryModel
{
    public AllAuthorsQueryModel()
    {
        this.Categories = new HashSet<string>();
        this.Authors = new HashSet<AuthorViewModel>();

        CurrentPage = DefaultPage;
        AuthorsPerPage = EntitiesPerPage;
    }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Search by text")]
    public string SearchTerm { get; set; } = null!;

    [Display(Name = "Sort by")]
    public AuthorSorting SortingOption { get; set; }

    public int CurrentPage { get; set; }

    [Display(Name = "Show on Page")]
    public int AuthorsPerPage { get; set; }

    public int TotalAutrhosCount { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<AuthorViewModel> Authors { get; set; }
}