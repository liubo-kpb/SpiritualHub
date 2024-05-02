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

        _validationService.GetUserIdFunc = () => userId;
        _validationService.IsUserAdminFunc = () => false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        int expectedCallCount = 1;

        // Act
        string actualId = string.Empty;
        int actualCallCount = 0;

        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            actualId = id;
            return Task.FromResult(authorId);
        };

        _validationService.IsUserAdminFunc = () => {
            ++actualCallCount;
            return false;
        };

        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(actualId, Is.EqualTo(id));
            Assert.That(actualCallCount, Is.EqualTo(expectedCallCount));
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

        int expectedCallCount = 1;

        // Act
        int actualCallCout = 0;
        _validationService.IsUserAdminFunc = () => {
            ++actualCallCout;
            return true;
        };

        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(actualCallCout, Is.EqualTo(expectedCallCount));
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

        _validationService.GetUserIdFunc = () => userId;
        _validationService.IsUserAdminFunc = () => false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(false);

        int expectedCallCount = 1;
        string expectedUrl = string.Format(_url, "Details", nameof(Author));
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualId = string.Empty;
        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;
        int actualCallCount = 0;

        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            actualId = id;
            return Task.FromResult(authorId);
        };

        _validationService.IsUserAdminFunc = () => {
            ++actualCallCount;
            return false;
        };

        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualMessage = message;
            actualType = type;
        };

        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(actualId, Is.EqualTo(id));
            Assert.That(actualCallCount, Is.EqualTo(expectedCallCount));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
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

        _validationService.GetUserIdFunc = () => userId;
        _validationService.IsUserAdminFunc = () => false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(false);

        int expectedCallCount = 1;
        string expectedUrl = string.Format(_url, "Become", nameof(Publisher));
        string expectedErrorMessage = NotAPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;
        int actualCallCount = 0;

        _validationService.IsUserAdminFunc = () => {
            ++actualCallCount;
            return false;
        };

        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualMessage = message;
            actualType = type;
        };

        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(actualCallCount, Is.EqualTo(expectedCallCount));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
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

        _validationService.GetUserIdFunc = () => userId;
        _validationService.IsUserAdminFunc = () => false;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(true);

        int expectedCallCount = 1;
        int expectedGetAuthorIdCallCount = 0;

        // Act
        int actualCallCount = 0;
        int getAuthorIdCallCount = 0;

        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            ++getAuthorIdCallCount;
            return null!;
        };

        _validationService.IsUserAdminFunc = () => {
            ++actualCallCount;
            return false;
        };

        var result = await _validationService.CheckModifyPermissionsAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(actualCallCount, Is.EqualTo(expectedCallCount));
            Assert.That(getAuthorIdCallCount, Is.EqualTo(expectedGetAuthorIdCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id)));
    }
}
