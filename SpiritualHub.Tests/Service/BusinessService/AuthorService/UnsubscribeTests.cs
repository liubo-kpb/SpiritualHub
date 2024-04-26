namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Moq;

public class UnsubscribeTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithSubscriber(testUser);

        string userId = testUser.Id.ToString();
        string authorId = testAuthor.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(testUser);

        int expectedAuthorSubscriberCount = testAuthor.Subscriptions.Sum(s => s.Subscribers.Count) - 1;

        // Act
        await _authorService.UnsubscribeAsync(authorId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Subscriptions.All(s => s.Subscribers.All(ss => ss.Id != testUser.Id)), "User is still subscribed to author.");
            Assert.That(testAuthor.Subscriptions.Sum(s => s.Subscribers.Count), Is.EqualTo(expectedAuthorSubscriberCount), "Expected subscriber count does not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenNotSubscribed()
    {
        // Arrange
        var testUser = _users.First();
        var testSubscriber = _users[2];
        var testAuthor = GetAuthorWithSubscriber(testSubscriber);

        string userId = testUser.Id.ToString();
        string authorId = testAuthor.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(testUser);

        int expectedAuthorFollowerCount = testAuthor.Subscriptions.Sum(s => s.Subscribers.Count);

        // Act
        await _authorService.UnsubscribeAsync(authorId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Subscriptions.All(s => s.Subscribers.All(ss => ss.Id != testUser.Id)), "User is subscribed to author when it shouldn't be.");
            Assert.That(testAuthor.Subscriptions.Sum(s => s.Subscribers.Count), Is.EqualTo(expectedAuthorFollowerCount), "Expected subscriber count does not match.");
            Assert.That(testAuthor.Subscriptions.Any(s => s.Subscribers.Any(ss => ss.Id == testSubscriber.Id)), "The wrong user was removed from the subscribers.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
