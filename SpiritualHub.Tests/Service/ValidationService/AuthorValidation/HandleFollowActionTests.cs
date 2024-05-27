namespace SpiritualHub.Tests.Service.ValidationService.AuthorValidation;

using Moq;

using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;


public class HandleFollowActionTests : MockConfiguration
{
    [Test]
    public async Task WhenValid()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCheckCallCount = 1;
        int expectedGetUserIdCallCount = 1;

        _authorServiceMock.Setup(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId))).ReturnsAsync(false);

        // Act
        int existsCheckCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCheckCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCheckCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _authorServiceMock.Verify(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId)));
    }

    [Test]
    public async Task WhenAuthorDoesNotExist()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCheckCallCount = 1;
        int expectedGetUserIdCallCount = 0;
        var expectedNotificationType = NotificationType.ErrorMessage;
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        string expectedUrl = string.Format(_url, ControllerName, "All");

        // Act
        int existsCheckCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCheckCallCount++;
            actualId = id;
            return Task.FromResult(false);
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        var actualType = NotificationType.Null;
        string actualMessage = string.Empty;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualType = type;
            actualMessage = message;
        };

        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCheckCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _authorServiceMock.Verify(x => x.IsFollowedByUserWithId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenAlreadyFollowing()
    {
        // Arrange
        string authorId = "authorId";
        string userId = "userId";

        int expectedExistsCheckCallCount = 1;
        int expectedGetUserIdCallCount = 1;
        var expectedNotificationType = NotificationType.ErrorMessage;
        string expectedErrorMessage = AlreadyFollowingAuthorErrorMessage;
        string expectedUrl = string.Format(_url, ControllerName, "Mine");

        _authorServiceMock.Setup(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId))).ReturnsAsync(true);

        // Act
        int existsCheckCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCheckCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int getUserIdCallCount = 0;
        _validationService.GetUserIdFunc = () =>
        {
            getUserIdCallCount++;
            return userId;
        };

        var actualType = NotificationType.Null;
        string actualMessage = string.Empty;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualType = type;
            actualMessage = message;
        };

        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(actualId, Is.EqualTo(authorId));
            Assert.That(existsCheckCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _authorServiceMock.Verify(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId)));
    }
}
