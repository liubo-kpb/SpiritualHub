namespace SpiritualHub.Tests.Service.ValidationService.BaseValidation;

using Client.Infrastructure.Enums;

using static Common.ErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class ExistsTests : MockConfiguration
{
    [Test]
    public async Task WhenExists()
    {
        // Arrange
        string expectedId = "testId";

        // Act
        string actualId = string.Empty;
        _validationService.ExistsAsyncFunc = async (id) =>
        {
            actualId = id;
            return await Task.FromResult(true);
        };

        var result = await _validationService.HandleExistsCheckAsync(expectedId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(actualId, Is.EqualTo(expectedId), string.Format(WrongVariableValueErrorMessage, "Id"));
        });
    }

    [Test]
    public async Task WhenDoesNotExist()
    {
        // Arrange
        string action = "All";

        string expectedUrl = string.Format(_url, action, ControllerName);
        string expectedId = "testId";
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        string actualId = string.Empty;
        string actualMessage = string.Empty;
        var actualType = NotificationType.Null;

        _validationService.ExistsAsyncFunc = async (id) =>
        {
            actualId = id;
            return await Task.FromResult(false);
        };

        _validationService.SetTempDataMessageAction = (type, message) =>
        {
            actualType = type;
            actualMessage = message;
        };

        var result = await _validationService.HandleExistsCheckAsync(expectedId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl));
            Assert.That(actualId, Is.EqualTo(expectedId), string.Format(WrongVariableValueErrorMessage, "Id"));
            Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage), string.Format(WrongVariableValueErrorMessage, "Error message"));
            Assert.That(actualType, Is.EqualTo(expectedNotificationType), string.Format(WrongVariableValueErrorMessage, "Notification Type"));
        });
    }
}
