namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation;

using Moq;

using Data.Models;
using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class CheckPublisherConnectionToAuthorTests : MockConfiguration
{
    [Test]
    public async Task WhenTrue_WithAuthorId()
    {
        // Arrange
        string id = "authorId";
        bool isAuthorId = true;

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(true);

        // Act
        var result = await _validationService.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
        });
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id)));
    }

    [Test]
    public async Task WhenTrue_WithoutAuthorId()
    {
        // Arrange
        string id = "id";
        bool isAuthorId = false;

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        string authorId = "authorId";

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(true);

        // Act
        var result = await _validationService.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id));
        });
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }

    [Test]
    public async Task WhenFalse_WithAuthorId()
    {
        // Arrange
        string id = "authorId";
        bool isAuthorId = true;

        string userId = "userId";

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(false);

        string expectedUrl = string.Format(_url, nameof(Author), "Details");
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl), string.Format(WrongVariableValueErrorMessage, "Url"));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage), string.Format(WrongVariableValueErrorMessage, "Error message"));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType), string.Format(WrongVariableValueErrorMessage, "Notification Type"));
        });
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id)));
    }

    [Test]
    public async Task WhenFalse_WithoutAuthorId()
    {
        // Arrange
        string id = "id";
        bool isAuthorId = false;

        string userId = "userId";

        string authorId = "authorId";

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId))).ReturnsAsync(false);

        string expectedUrl = string.Format(_url, nameof(Author), "Details");
        string expectedErrorMessage = NotAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Not.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl), string.Format(WrongVariableValueErrorMessage, "Url"));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(id), string.Format(WrongVariableValueErrorMessage, "Id"));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage), string.Format(WrongVariableValueErrorMessage, "Error message"));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType), string.Format(WrongVariableValueErrorMessage, "Notification Type"));
        });
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == authorId)));
    }
}
