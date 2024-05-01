namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation;

using Moq;

using Data.Models;
using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;

public class CheckUserIsPublisherTests : MockConfiguration
{
    [Test]
    public async Task WhenTrue()
    {
        // Arrange
        string userId = "testId";
        _validationService.GetUserIdFunc = () => userId;
        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(true);

        // Act
        var result = await _validationService.CheckUserIsPublisherAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }

    [Test]
    public async Task WhenFalse()
    {
        // Arrange
        string userId = "testId";
        _validationService.GetUserIdFunc = () => userId;
        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(false);

        string expectedUrl = string.Format(_url, "Become", nameof(Publisher));
        string expectedMessage = NotAPublisherErrorMessage;
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;
        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualType = type;
            actualMessage = message;
        };

        var result = await _validationService.CheckUserIsPublisherAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(actualMessage, Is.EqualTo(expectedMessage));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == userId)));
    }
}
