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
        var result = await _validationService.HandleExistsCheckAsync(expectedId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(expectedId), string.Format(WrongVariableValueErrorMessage, "Id"));
        });
    }

    [Test]
    public async Task WhenDoesNotExist()
    {
        // Arrange
        string action = "All";

        _validationService.Exists = false;

        string expectedUrl = string.Format(_url, ControllerName, action);
        string expectedId = "testId";
        string expectedErrorMessage = string.Format(NoEntityFoundErrorMessage, _validationService.EntityName);
        var expectedNotificationType = NotificationType.ErrorMessage;

        // Act
        var result = await _validationService.HandleExistsCheckAsync(expectedId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(_validationService.RouteValue, Is.Null);
            Assert.That(_validationService.ActionUrl, Is.EqualTo(expectedUrl), string.Format(WrongVariableValueErrorMessage, "Url"));
            Assert.That(_validationService.ActualEntityId, Is.EqualTo(expectedId), string.Format(WrongVariableValueErrorMessage, "Id"));
            Assert.That(_validationService.ActualErrorMessage, Is.EqualTo(expectedErrorMessage), string.Format(WrongVariableValueErrorMessage, "Error message"));
            Assert.That(_validationService.ActualNotificationType, Is.EqualTo(expectedNotificationType), string.Format(WrongVariableValueErrorMessage, "Notification Type"));
        });
    }
}
