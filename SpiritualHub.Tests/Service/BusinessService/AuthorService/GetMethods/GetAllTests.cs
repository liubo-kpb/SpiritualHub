namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.GetMethods;

using System.Linq;

using Moq;
using MockQueryable.Moq;

using Client.ViewModels.Author;
using Client.Infrastructure.Enums;
using Services.Mappings;
using Data.Models;
using Data.Configuration.Seed;

public class GetAllTests : MockConfiguration
{
    [Test]
    [TestCase(1, 3, "", "", AuthorSorting.Newest)] // Regular search
    [TestCase(1, 20, "", "")] // When we wish to load more than the DBs entity count.
    [TestCase(2, 20, "", "")] // If we want to go to the second page of authors that exceed the DBs entities.
    [TestCase(1, 10, "", "", AuthorSorting.Oldest)]
    [TestCase(1, 10, "", "", AuthorSorting.FollowersDescending)]
    [TestCase(1, 10, "", "", AuthorSorting.FollowersDescending)]
    [TestCase(1, 10, "", "", AuthorSorting.SubscribersDescending)]
    [TestCase(1, 10, "", "", AuthorSorting.SubscribersAscending)]
    [TestCase(1, 10, "", "O")]
    [TestCase(1, 10, "Sp", "")]
    [TestCase(1, 10, "Sp", "O")]
    [TestCase(1, 10, "!@#$%", "!@#$%")]
    public async Task MultipleCases(int page, int entitiesPerPage, string categoryName, string searchTerm, AuthorSorting sortingOption = AuthorSorting.Newest)
    {
        // Arrange
        var queryModel = new AllAuthorsQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _authors.Count,
            SortingOption = sortingOption,
        };

        var testUser = _users.First();
        var testAuthor1 = GetAuthorWithFollower(testUser);
        var testAuthor2 = GetAuthorWithSubscriber();
        SetCategoriesToAuthors();

        var expectedAuthorsList = new List<AuthorViewModel>();
        _mapper.MapListToViewModel(FilterExpectedAuthors(queryModel), expectedAuthorsList);

        var expectedFollowedAuthor = expectedAuthorsList.FirstOrDefault(a => a.Id == testAuthor1.Id.ToString());
        if (expectedFollowedAuthor != null)
        {
            expectedFollowedAuthor.FollowerCount = 1;
            expectedFollowedAuthor.IsUserFollowing = true;
        }

        var expectedSubscribedAuthor = expectedAuthorsList.FirstOrDefault(a => a.Id == testAuthor2.Id.ToString());
        if (expectedSubscribedAuthor != null)
        {
            expectedSubscribedAuthor.SubscriberCount = 1;
        }

        _authorRepositoryMock.Setup(x => x.GetAll()).Returns(_authors.AsQueryable().BuildMock());

        // Act
        var result = await _authorService.GetAllAsync(queryModel, testUser.Id.ToString());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Authors, Is.EqualTo(expectedAuthorsList));
            Assert.That(result.TotalAuthorsCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _authorRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    [Test]
    [TestCase(2, 3, "", "")]
    public async Task WhenOnSecondPage_DifferentEntittiesVerification(int page, int entitiesPerPage, string categoryName, string searchTerm, AuthorSorting sortingOption = AuthorSorting.Newest)
    {
        // Arrange
        var queryModel = new AllAuthorsQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _authors.Count,
            SortingOption = sortingOption,
        };

        var testUser = _users.First();
        var testAuthor1 = GetAuthorWithFollower(testUser);
        var testAuthor2 = GetAuthorWithSubscriber();
        SetCategoriesToAuthors();

        var expectedAuthorsList = new List<AuthorViewModel>();
        _mapper.MapListToViewModel(FilterExpectedAuthors(queryModel), expectedAuthorsList);

        var notExpectedAuthorsList = new List<AuthorViewModel>();
        var wrongQuery = new AllAuthorsQueryModel()
        {
            CategoryName = string.Empty,
            SearchTerm = string.Empty,
            CurrentPage = 1,
            EntitiesPerPage = 3,
            TotalEntitiesCount = _authors.Count,
            SortingOption = AuthorSorting.Newest,
        };
        _mapper.MapListToViewModel(FilterExpectedAuthors(wrongQuery), notExpectedAuthorsList);

        var expectedFollowedAuthor = expectedAuthorsList.FirstOrDefault(a => a.Id == testAuthor1.Id.ToString());
        if (expectedFollowedAuthor != null)
        {
            expectedFollowedAuthor.FollowerCount = 1;
            expectedFollowedAuthor.IsUserFollowing = true;
        }

        var expectedSubscribedAuthor = expectedAuthorsList.FirstOrDefault(a => a.Id == testAuthor2.Id.ToString());
        if (expectedSubscribedAuthor != null)
        {
            expectedSubscribedAuthor.SubscriberCount = 1;
        }

        _authorRepositoryMock.Setup(x => x.GetAll()).Returns(_authors.AsQueryable().BuildMock());

        // Act
        var result = await _authorService.GetAllAsync(queryModel, testUser.Id.ToString());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Authors, Is.EqualTo(expectedAuthorsList));
            Assert.That(result.Authors, Is.Not.EqualTo(notExpectedAuthorsList));
            Assert.That(result.TotalAuthorsCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _authorRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    private void SetCategoriesToAuthors()
    {
        var categoryList = new SeedCategoryConfiguration().GenerateEntities();
        foreach (var author in _authors)
        {
            author.Category = categoryList.FirstOrDefault(c => c.Id == author.CategoryID);
        }
    }

    private IEnumerable<Author> FilterExpectedAuthors(AllAuthorsQueryModel queryModel)
    {
        IEnumerable<Author> filteredAuthors = _authors;
        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            filteredAuthors = filteredAuthors.Where(a => a.Category!.Name.ToLower().Contains(queryModel.CategoryName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            var searchTermToLower = queryModel.SearchTerm.ToLower();
            filteredAuthors = filteredAuthors.Where(a => a.Alias.ToLower().Contains(searchTermToLower)
                                                        || a.Name.ToLower().Contains(searchTermToLower)
                                                        || a.Description.ToLower().Contains(searchTermToLower));
        }

        filteredAuthors = queryModel.SortingOption switch
        {
            AuthorSorting.Newest => filteredAuthors.OrderByDescending(a => a.AddedOn),
            AuthorSorting.Oldest => filteredAuthors.OrderBy(a => a.AddedOn),
            AuthorSorting.FollowersDescending => filteredAuthors.OrderByDescending(a => a.Followers.Count),
            AuthorSorting.FollowersAscending => filteredAuthors.OrderBy(a => a.Followers.Count),
            AuthorSorting.SubscribersDescending => filteredAuthors.OrderByDescending(a => a.Subscriptions.Count),
            AuthorSorting.SubscribersAscending => filteredAuthors.OrderBy(a => a.Subscriptions.Count),
            _ => filteredAuthors.Where(a => a.IsActive)
                             .OrderByDescending(a => a.Followers.Count)
                             .ThenByDescending(a => a.AddedOn)
        };

        return filteredAuthors
            .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
            .Take(queryModel.EntitiesPerPage);
    }
}