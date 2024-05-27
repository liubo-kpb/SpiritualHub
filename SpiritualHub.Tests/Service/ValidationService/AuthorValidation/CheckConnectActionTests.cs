namespace SpiritualHub.Tests.Service.ValidationService.AuthorValidation;

using Moq;

using Client.Infrastructure.Enums;
using Data.Models;

using static Common.ErrorMessagesConstants;

public class CheckConnectActionTests : MockConfiguration
{
    [Test]
    public async Task WhenValidRequest()
    {
        // Arrange
        var id = _validationService.AuthorId;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == _validationService.UserId), It.Is<string>(x => x == id))).ReturnsAsync(false);

        // Act
        var result = await _validationService.CheckConnectActionAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(1));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(1));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(2));

            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(NotificationType.Null));
            Assert.That(_validationService.ActualErrorMessage, Is.Null);
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == _validationService.UserId), It.Is<string>(x => x == id)));
    }

    [Test]
    public async Task WhenUserIsAdmin()
    {
        // Arrange
        var id = _validationService.AuthorId;
        _validationService.IsAdmin = true;

        // Act
        var result = await _validationService.CheckConnectActionAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(1));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(1));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(0));

            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(NotificationType.Null));
            Assert.That(_validationService.ActualErrorMessage, Is.Null);
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenAuthorDoesNotExist()
    {
        // Arrange
        var id = _validationService.AuthorId;
        _validationService.Exists = false;

        var expectedUrl = string.Format(_url, ControllerName, "All");
        var expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);

        // Act
        var result = await _validationService.CheckConnectActionAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(0));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(1));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(0));

            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(NotificationType.ErrorMessage));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenUserIsNotPublisher()
    {
        // Arrange
        var id = _validationService.AuthorId;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId))).ReturnsAsync(false);

        var expectedUrl = string.Format(_url, nameof(Publisher), "Become");

        // Act
        var result = await _validationService.CheckConnectActionAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(1));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(1));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(1));

            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(NotificationType.ErrorMessage));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(NotAPublisherErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task WhenUserIsConnectedPublisher()
    {
        // Arrange
        var id = _validationService.AuthorId;

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == _validationService.UserId), It.Is<string>(x => x == id))).ReturnsAsync(true);


        var expectedUrl = string.Format(_url, ControllerName, "MyPublishings");

        // Act
        var result = await _validationService.CheckConnectActionAsync(id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
            Assert.That(_validationService.AdminCheckCallCount, Is.EqualTo(1));
            Assert.That(_validationService.ExistsCallCount, Is.EqualTo(1));
            Assert.That(_validationService.GetUserIdCallCount, Is.EqualTo(2));

            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(NotificationType.ErrorMessage));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(AlreadyAConnectedPublisherErrorMessage));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == _validationService.UserId)));
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == _validationService.UserId), It.Is<string>(x => x == id)));
    }
}
