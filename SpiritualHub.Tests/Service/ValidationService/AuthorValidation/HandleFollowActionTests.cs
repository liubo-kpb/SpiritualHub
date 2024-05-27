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
        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
        });
        _authorServiceMock.Verify(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId)));
    }

    [Test]
    public async Task WhenAuthorDoesNotExist()
    {
        // Arrange
        string authorId = "authorId";

        _validationService.Exists = false;

        int expectedExistsCheckCallCount = 1;
        int expectedGetUserIdCallCount = 0;
        var expectedNotificationType = NotificationType.ErrorMessage;
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        string expectedUrl = string.Format(_url, ControllerName, "All");

        // Act
        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
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
        var result = await _validationService.HandleFollowActionAsync(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(authorId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCheckCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
        });
        _authorServiceMock.Verify(x => x.IsFollowedByUserWithId(It.Is<string>(x => x == authorId), It.Is<string>(x => x == userId)));
    }
}
