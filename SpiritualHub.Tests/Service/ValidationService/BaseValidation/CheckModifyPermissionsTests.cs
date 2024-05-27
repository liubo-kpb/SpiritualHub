namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation;

using Moq;

using Data.Models;
using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;

public class CheckModifyPermissionsTests : MockConfiguration
{
    [Test]
    public async Task WhenIsConnectedPublisher()
    {
        // Arrange
        string id = "id";
        string userId = "userId";
        string authorId = "authorId";
        bool isAuthorId = false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        int expectedCallCount = 1;

        // Act
        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenUserIsAdmin()
    {
        // Arrange
        string id = "id";
        bool isAuthorId = false;

        _validationService.IsAdmin = true;

        int expectedCallCount = 1;

        // Act
        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenNotConnectedPublisherAndAdmin()
    {
        // Arrange
        string id = "id";
        string userId = "userId";
        string authorId = "authorId";
        bool isAuthorId = false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(false);

        int expectedCallCount = 1;
        string expectedUrl = string.Format(_url, nameof(Author), "Details");
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedCallCount));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenNotPublisherAndAdmin()
    {
        // Arrange
        string id = "id";
        string userId = "userId";
        bool isAuthorId = false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(false);

        int expectedCallCount = 1;
        string expectedUrl = string.Format(_url, nameof(Publisher), "Become");
        string expectedErrorMessage = NotAPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedCallCount));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenIsConnectedPublisherWithAuthorId()
    {
        // Arrange
        string id = "authorId";
        string userId = "userId";
        bool isAuthorId = true;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(true);

        int expectedCallCount = 1;

        // Act
        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id)));
    }
}
