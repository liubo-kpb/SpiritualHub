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

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        int existsCallCountExpectation = 1;
        int adminCheckCallCountExpectation = 2;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(existsCallCountExpectation));

            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(adminCheckCallCountExpectation));
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

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(existsCallCountExpectation));

            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(adminCheckCallCountExpectation));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenIsAdmin_NoAuthorId()
    {
        // Arrange
        string entityId = "id";

        _validationService.IsAdmin = true;

        int expectedAdminCheckCallCount = 1;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenEntityDoesNotExist()
    {
        // Arrange
        string entityId = "wrongId";

        _validationService.Exists = false;

        string expectedUrl = string.Format(_url, ControllerName, "All");
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));

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

        string expectedUrl = string.Format(_url, nameof(Publisher), "Become");
        string expectedErrorMessage = NotAPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
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

        string expectedUrl = string.Format(_url, nameof(Author), "Details");
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, null);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
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

        int expectedExistsCallCount = 1;
        int expectedGetUserIdCallCount = 2;
        int expectedAdminCheckCallCount = 2;

        string expectedUrl = string.Format(_url, nameof(Author), "Details");
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckModifyActionAsync(entityId, authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(expectedAdminCheckCallCount));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(expectedExistsCallCount));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(expectedGetUserIdCallCount));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(entityId));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }
}
