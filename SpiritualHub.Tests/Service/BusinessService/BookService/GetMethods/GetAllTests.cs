namespace SpiritualHub.Tests.Service.BusinessService.BookService.GetMethods;

using Moq;
using MockQueryable.Moq;

using Client.Infrastructure.Enums;
using Client.ViewModels.Book;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;
using SpiritualHub.Client.ViewModels.Author;

public class GetAllTests : MockConfiguration
{
    [Test]
    [TestCase(1, 3, "", "", BookSorting.Newest)] // Regular search
    [TestCase(1, 20, "", "")] // When we wish to load more than the DBs entity count.
    [TestCase(2, 20, "", "")] // If we want to go to the second page of authors that exceed the DBs entities.
    [TestCase(1, 10, "", "", BookSorting.Oldest)]
    [TestCase(1, 10, "", "", BookSorting.TopRated)]
    [TestCase(1, 10, "", "", BookSorting.LeastRated)]
    [TestCase(1, 10, "", "", BookSorting.PriceAscending)]
    [TestCase(1, 10, "", "", BookSorting.PriceDescending)]
    [TestCase(1, 10, "", "O")]
    [TestCase(1, 10, "Masters", "")]
    [TestCase(1, 10, "Sp", "O")]
    [TestCase(1, 10, "!@#$%", "!@#$%")]
    public async Task MultipleCases(int page, int entitiesPerPage, string categoryName, string searchTerm, BookSorting sortingOption = BookSorting.Newest)
    {
        // Arrange
        var queryModel = new AllBooksQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _books.Count - 1,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        _books[2].IsHidden = true;

        GetBookWithReader();
        AssignBookForeignEntities();


        var expectedList = new List<BookViewModel>();
        _mapper.MapListToViewModel(FilterExpectedBooks(queryModel), expectedList);

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_books.AsQueryable().BuildMock());

        // Act
        var result = await _bookService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Books, Is.EqualTo(expectedList));
            Assert.That(result.TotalBooksCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);

    }

    [Test]
    [TestCase(2, 3, "", "")]
    public async Task WhenOnSecondPage_DifferentEntittiesVerification(int page, int entitiesPerPage, string categoryName, string searchTerm, BookSorting sortingOption = BookSorting.Newest)
    {
        // Arrange
        var queryModel = new AllBooksQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _books.Count,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        GetBookWithReader();
        AssignBookForeignEntities();

        var expectedList = new List<BookViewModel>();
        _mapper.MapListToViewModel(FilterExpectedBooks(queryModel), expectedList);

        var wrongQuery = new AllBooksQueryModel()
        {
            CategoryName = string.Empty,
            SearchTerm = string.Empty,
            CurrentPage = 1,
            EntitiesPerPage = 6,
            TotalEntitiesCount = _books.Count,
            SortingOption = BookSorting.Oldest,
        };

        var notExpectedList = new List<BookViewModel>();
        _mapper.MapListToViewModel(FilterExpectedBooks(wrongQuery), notExpectedList);


        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_books.AsQueryable().BuildMock());

        // Act
        var result = await _bookService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Books, Is.EqualTo(expectedList));
            Assert.That(result.Books, Is.Not.EqualTo(notExpectedList));
            Assert.That(result.TotalBooksCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    private IEnumerable<Book> FilterExpectedBooks(AllBooksQueryModel queryModel)
    {
        IEnumerable<Book> filteredBooks = _books.Where(b => !b.IsHidden);
        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            filteredBooks = filteredBooks.Where(b => b.Category != null && b.Category!.Name.ToLower().Contains(queryModel.CategoryName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = queryModel.SearchTerm.ToLower();
            filteredBooks = filteredBooks.Where(b => b.Title.ToLower().Contains(wildCard)
                                            || b.Author.Name.ToLower().Contains(wildCard)
                                            || b.Description.ToLower().Contains(wildCard)
                                            || b.ShortDescription.ToLower().Contains(wildCard));
        }

        filteredBooks = queryModel.SortingOption switch
        {
            BookSorting.TopRated => filteredBooks.OrderByDescending(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (double) b.Ratings.Count)),
            BookSorting.LeastRated => filteredBooks.OrderBy(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (double) b.Ratings.Count)),
            BookSorting.Newest => filteredBooks.OrderByDescending(b => b.AddedOn),
            BookSorting.Oldest => filteredBooks.OrderBy(b => b.AddedOn),
            BookSorting.PriceAscending => filteredBooks.OrderBy(b => b.Price),
            BookSorting.PriceDescending => filteredBooks.OrderByDescending(b => b.Price),
            _ => filteredBooks.OrderByDescending(b => b.AddedOn)
                           .ThenByDescending(b => b.Ratings.Count == 0 ? 0 : (b.Ratings.Sum(r => r.Stars) / (b.Ratings.Count * 1.0)))
        };

        return filteredBooks
                    .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
                    .Take(queryModel.EntitiesPerPage);
    }

    private void AssignBookForeignEntities()
    {
        var categoryList = new SeedCategoryConfiguration().GenerateEntities();
        foreach (var book in _books)
        {
            book.Category = categoryList.FirstOrDefault(c => c.Id == book.CategoryID);
            book.Author = _authors.FirstOrDefault(a => a.Id == book.AuthorID)!;
        }
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
