namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.GetMethods;

using Moq;
using NuGet.Packaging;

using Client.ViewModels.Author;
using Data.Configuration.Seed;
using Data.Models;

public class AllAuthorsByUserIdTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_FollowsAuthors()
    {
        // Arrange
        var testUser = _users[0];
        var testAuthor = GetAuthorWithFollower(testUser);

        string userId = testUser.Id.ToString();

        var expected = new List<AuthorViewModel>()
        {
            new AuthorViewModel
            {
                Id = testAuthor.Id.ToString(),
                Alias = testAuthor.Alias,
                Name = testAuthor.Name,
                Description = testAuthor.Description,
                IsActive = testAuthor.IsActive,
                IsUserFollowing = true,
                FollowerCount = 1,
            },
        };

        _authorRepositoryMock
            .Setup(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)))
            .Returns(Task.FromResult(new List<Author>() { testAuthor })!);

        // Act
        var result = await _authorService.AllAuthorsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)));
    }

    [Test]
    public async Task WhenSuccess_SubscribedToAuthors()
    {
        // Arrange
        var testUser = _users[1];
        var testAuthor = GetAuthorWithSubscriber(testUser);

        string userId = testUser.Id.ToString();
        _authorRepositoryMock
            .Setup(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)))
            .Returns(Task.FromResult(new List<Author>() { testAuthor })!);

        var expected = new List<AuthorViewModel>()
        {
            new AuthorViewModel
            {
                Id = testAuthor.Id.ToString(),
                Alias = testAuthor.Alias,
                Name = testAuthor.Name,
                Description = testAuthor.Description,
                IsActive = testAuthor.IsActive,
                IsUserSubscribed = true,
                SubscriberCount = 1,
            },
        };

        // Act
        var result = await _authorService.AllAuthorsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)));
    }

    [Test]
    public async Task WhenSuccess_FollowsAndSubscribedToAuthors()
    {
        // Arrange
        var testUser = _users[1];
        var testAuthorWithFollow = GetAuthorWithFollower(testUser);
        var testAuthorWithSubscription = GetAuthorWithSubscriber(testUser);

        var expected = new List<AuthorViewModel>()
        {
            new AuthorViewModel
            {
                Id = testAuthorWithFollow.Id.ToString(),
                Alias = testAuthorWithFollow.Alias,
                Name = testAuthorWithFollow.Name,
                Description = testAuthorWithFollow.Description,
                IsActive = testAuthorWithFollow.IsActive,
                IsUserFollowing = true,
                FollowerCount = 1,
            },
            new AuthorViewModel
            {
                Id = testAuthorWithSubscription.Id.ToString(),
                Alias = testAuthorWithSubscription.Alias,
                Name = testAuthorWithSubscription.Name,
                Description = testAuthorWithSubscription.Description,
                IsActive = testAuthorWithSubscription.IsActive,
                IsUserSubscribed = true,
                SubscriberCount = 1,
            },
        };

        var authorSubscriptionPlans = new SeedSubscriptionConfiguration().GenerateEntities().Where(s => s.AuthorID == testAuthorWithSubscription.Id);
        testAuthorWithSubscription.Subscriptions.AddRange(authorSubscriptionPlans);
        testAuthorWithSubscription.Subscriptions.First().Subscribers.Add(testUser);

        string userId = testUser.Id.ToString();
        _authorRepositoryMock
            .Setup(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)))
            .ReturnsAsync(new List<Author>() { testAuthorWithFollow, testAuthorWithSubscription });

        // Act
        var result = await _authorService.AllAuthorsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)));
    }

    [Test]
    public async Task WhenSuccess_NoAuthors()
    {
        // Arrange
        string userId = _users[1].Id.ToString();

        _authorRepositoryMock
            .Setup(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)))
            .Returns(Task.FromResult(new List<Author>())!);

        var expected = new List<AuthorViewModel>();

        // Act
        var result = await _authorService.AllAuthorsByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllAuthorsByUserIdAsync(It.Is<string>(y => y == userId)));
    }
}
