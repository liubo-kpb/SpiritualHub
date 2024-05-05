namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation;

using Moq;

using Client.Infrastructure.Enums;
using Data.Models;

using static Common.ErrorMessagesConstants;

public class CheckModifyActionTests : MockConfiguration
{
    [Test]
    public async Task WhenConnectedPublisher_NoAuthorId()
    {
        // Arrange
        string entityId = "id";
        string authorId = "authorId";

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        int existsCallCountExpectation = 1;
        int adminCheckCallCountExpectation = 2;

        // Act
        int existsCheckCallCount = 0;
        string actualEntityId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            actualEntityId = id;
            existsCheckCallCount++;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
        };

        string actualEntityId2 = string.Empty;
        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            actualEntityId2 = id;
            return Task.FromResult(authorId);
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(actualEntityId, Is.EqualTo(entityId));
            Assert.That(existsCheckCallCount, Is.EqualTo(existsCallCountExpectation));

            Assert.That(adminCheckCallCount, Is.EqualTo(adminCheckCallCountExpectation));

            Assert.That(actualEntityId2, Is.EqualTo(entityId));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenConnectedPublisher_WithAuthorId()
    {
        // Arrange
        string entityId = "id";
        string authorId = "authorId";

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        int existsCallCountExpectation = 1;
        int adminCheckCallCountExpectation = 2;
        int getAuthorIdCallCountExpectation = 0;

        // Act
        int existsCheckCallCount = 0;
        string actualEntityId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            actualEntityId = id;
            existsCheckCallCount++;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
        };

        string actualEntityId2 = string.Empty;
        int getAuthorIdCallCount = 0;
        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            actualEntityId2 = id;
            getAuthorIdCallCount++;
            return Task.FromResult(authorId);
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(actualEntityId, Is.EqualTo(entityId));
            Assert.That(existsCheckCallCount, Is.EqualTo(existsCallCountExpectation));

            Assert.That(adminCheckCallCount, Is.EqualTo(adminCheckCallCountExpectation));

            Assert.That(actualEntityId2, Is.Not.EqualTo(entityId));
            Assert.That(getAuthorIdCallCount, Is.EqualTo(getAuthorIdCallCountExpectation));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenIsAdmin_NoAuthorId()
    {
        // Arrange
        string entityId = "id";

        int expectedAdminCheckCallCount = 1;

        // Act
        string actualEntityId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            actualEntityId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return true;
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(actualEntityId, Is.EqualTo(entityId));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenEntityDoesNotExist()
    {
        // Arrange
        string entityId = "wrongId";

        string expectedUrl = string.Format(_url, "All", ControllerName);
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            actualId = id;
            return Task.FromResult(false);
        };

        var actualType = NotificationType.Null;
        string actualMessage = string.Empty;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualType = type;
            actualMessage = message;
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualId, Is.EqualTo(entityId));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));

            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.RouteValue, Is.Null);
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenNotAPublisher()
    {
        // Arrange
        string entityId = "id";
        string userId = "userId";

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(false);

        int expectedExistsCallCount = 1;
        int expectedGetUserIdCallCount = 1;
        int expectedAdminCheckCallCount = 2;

        string expectedUrl = string.Format(_url, "Become", nameof(Publisher));
        string expectedErrorMessage = NotAPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
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

        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualId, Is.EqualTo(entityId));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenNotConnectedPublisher_NoAuthorId()
    {
        // Arrange
        string entityId = "id";
        string userId = "userId";
        string authorId = "authorId";

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(false);

        int expectedExistsCallCount = 1;
        int expectedGetUserIdCallCount = 2;
        int expectedAdminCheckCallCount = 2;

        string expectedUrl = string.Format(_url, "Details", nameof(Author));
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
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

        string actualEntityId2 = string.Empty;
        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            actualEntityId2 = id;
            return Task.FromResult(authorId);
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualId, Is.EqualTo(entityId));
            Assert.That(actualEntityId2, Is.EqualTo(entityId));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenNotConnectedPublisher_WithAuthorId()
    {
        // Arrange
        string entityId = "id";
        string userId = "userId";
        string authorId = "authorId";

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(false);

        int expectedGetAuthorIdCallCount = 0;
        int expectedExistsCallCount = 1;
        int expectedGetUserIdCallCount = 2;
        int expectedAdminCheckCallCount = 2;

        string expectedUrl = string.Format(_url, "Details", nameof(Author));
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        int existsCallCount = 0;
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = (id) =>
        {
            existsCallCount++;
            actualId = id;
            return Task.FromResult(true);
        };

        int adminCheckCallCount = 0;
        _validationService.IsUserAdminFunc = () =>
        {
            adminCheckCallCount++;
            return false;
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

        int getAuthorIdCallCount = 0;;
        _validationService.GetAuthorIdAsyncFunc = (id) =>
        {
            getAuthorIdCallCount++;
            return Task.FromResult(authorId);
        };

        var result = await _validationService.CheckModifyActionAsync(entityId, authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(adminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(existsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(getUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(actualId, Is.EqualTo(entityId));
            Assert.That(getAuthorIdCallCount, Is.EqualTo(expectedGetAuthorIdCallCount));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }
}
