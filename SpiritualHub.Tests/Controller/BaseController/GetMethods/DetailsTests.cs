namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.BaseModels;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.NotificationMessagesConstants;

using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class DetailsTests : MockConfiguration
{
    [Test]
    public async Task Details_WhenSuccess()
    {
        // Arrange
        var id = "id";
        var viewModel = new BaseDetailsViewModel();

        // Act
        var result = await Controller.Details(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1, 1, 1);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.EqualTo(viewModel));
        });
    }

    [Test]
    public async Task Details_WhenEntityDoesNotExist_ReturnsRedirectToAction()
    {
        // Arrange
        var id = "id";
        Controller.ExistsAsyncResult = false;

        // Act
        var result = await Controller.Details(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1, 0, 0);
            AssertTempData(string.Format(NoEntityFoundErrorMessage, EntityName));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("All"));
        });
    }

    [Test]
    public async Task Details_WhenValidateAccessibilityAsyncReturnsFalse_ReturnsRedirectToAction()
    {
        // Arrange
        var id = "id";

        Controller.CanAccessEntityDetials = false;

        // Act
        var result = await Controller.Details(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1, 1, 0);
            AssertTempData(MethodErrorMessage);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("All"));
        });
    }

    [Test]
    public async Task Details_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Details(id);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1, 1, 1);
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, $"loading {EntityName}"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("All"));
        });
    }

    private void AssertCounters(int expectedExistsCounter, int expectedAccessValidationCounter, int expectedGetEntityCounter)
    {
        Assert.That(Controller.ExistsAsyncCounter, Is.EqualTo(expectedExistsCounter), string.Format(WrongVariableValueErrorMessage, "ExistsCounter"));
        Assert.That(Controller.ValidateAccessibilityAsyncCounter, Is.EqualTo(expectedAccessValidationCounter), string.Format(WrongVariableValueErrorMessage, "AccessValidationCounter"));
        Assert.That(Controller.GetEntityDetailsAsyncCounter, Is.EqualTo(expectedGetEntityCounter), string.Format(WrongVariableValueErrorMessage, "EntityCounter"));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
