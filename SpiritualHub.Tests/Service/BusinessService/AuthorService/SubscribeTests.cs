namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Moq;

using static Extensions.Common.TestErrorMessagesConstants;
using static Common.ErrorMessagesConstants;

public class SubscribeTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_WhenChangingSubscription()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithSubscriber(testUser);

        string authorId = testAuthor.Id.ToString();
        string userId = testUser.Id.ToString();
        string subscriptionId = testAuthor
                                    .Subscriptions
                                    .First(s => s.Id != testAuthor
                                                        .Subscriptions
                                                        .First(s => s
                                                                    .Subscribers
                                                                    .Any(ss => ss.Id == testUser.Id))
                                                        .Id)
                                    .Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        // Act
        await _authorService.SubscribeAsync(authorId, subscriptionId, userId);

        // Assert
        _authorRepositoryMock.Verify(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId)),Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(),Times.Once);
    }

    [Test]
    public async Task WhenSuccess_WhenNewSubscription()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithSubscriber(_users[2]);

        string authorId = testAuthor.Id.ToString();
        string userId = testUser.Id.ToString();
        string subscriptionId = testAuthor.Subscriptions.First().Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        // Act
        await _authorService.SubscribeAsync(authorId, subscriptionId, userId);

        // Assert
        _authorRepositoryMock.Verify(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void WhenFail_AlreadyHasSubscription_ThrowTest()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithSubscriber(testUser);

        string authorId = testAuthor.Id.ToString();
        string userId = testUser.Id.ToString();
        string subscriptionId = testAuthor.Subscriptions.First().Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        // Act && Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _authorService.SubscribeAsync(authorId, subscriptionId, userId));
    }

    [Test]
    public async Task WhenFail_AlreadyHasSubscription_MethodCallTest()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithSubscriber(testUser);

        string authorId = testAuthor.Id.ToString();
        string userId = testUser.Id.ToString();
        string subscriptionId = testAuthor.Subscriptions.First().Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);

        var expectedErroMessage = SubscriptionAlreadySetErrorMessage;

        // Act
        try
        {
            await _authorService.SubscribeAsync(authorId, subscriptionId, userId);
        }
        // Assert
        catch (ArgumentException e)
        {
            _authorRepositoryMock.Verify(x => x.GetAuthorWithSubscriptionsAndSubscribersAsync(It.Is<string>(x => x == authorId)), Times.Once);
            _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
            Assert.That(e.Message, Is.EqualTo(expectedErroMessage));

            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoArgumentExceptionErrorMessage);
    }
}
