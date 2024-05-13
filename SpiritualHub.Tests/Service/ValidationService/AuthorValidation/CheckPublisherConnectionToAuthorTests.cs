namespace SpiritualHub.Tests.Service.ValidationService.AuthorValidation;

using Moq;

using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class CheckPublisherConnectionToAuthorTests : MockConfiguration
{
    [Test]
    public async Task WhenTrue()
    {
        // Arrange
        string id = "authorId";
        bool isAuthorId = true;

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(true);

        string expectedUrl = string.Format(_url, ControllerName, "MyPublishings");
        string expectedErrorMessage = AlreadyAConnectedPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualMessage = string.Empty;
        var actualType = NotificationType.ErrorMessage;

        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualMessage = message;
            actualType = type;
        };

        var result = await _validationService.CheckPublisherConnectionToAuthorAsync(id, isAuthorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);

            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl), string.Format(WrongVariableValueErrorMessage, "Url"));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage), string.Format(WrongVariableValueErrorMessage, "Error message"));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType), string.Format(WrongVariableValueErrorMessage, "Notification Type"));

            
        });
        _publisherServiceMock.Verify(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id)));
    }

    [Test]
    public async Task WhenFalse()
    {
        // Arrange
        string id = "authorId";
        bool isAuthorId = true;

        string userId = "userId";
        _validationService.GetUserIdFunc = () => userId;

        _publisherServiceMock.Setup(x => x.IsConnectedToAuthorByUserId(It.Is<string>(x => x == userId), It.Is<string>(x => x == id))).ReturnsAsync(false);

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
}
