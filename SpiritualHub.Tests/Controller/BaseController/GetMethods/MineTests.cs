namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using Microsoft.AspNetCore.Mvc;

using Client.ViewModels.BaseModels;

using static Common.ExceptionErrorMessagesConstants;
using static Common.NotificationMessagesConstants;

using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class MineTests : MockConfiguration
{
    [Test]
    public async Task Mine_WhenSuccess()
    {
        // Arrange
        var viewModel = new List<EmptyViewModel>();

        // Act
        var result = await Controller.Mine();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(((ViewResult) result).Model, Is.EqualTo(viewModel));
        });
    }

    [Test]
    public async Task Mine_WhenNotImplementedExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Mine();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(TestErrorMessageForExceptions);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    [Test]
    public async Task Mine_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Mine();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, $"load your {EntityName}s"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("All"));
        });
    }

    private void AssertCounters(int expectedGetAllEntitiesCounter)
    {
        Assert.That(Controller.GetAllEntitiesByUserIdAsyncCounter, Is.EqualTo(expectedGetAllEntitiesCounter), string.Format(WrongVariableValueErrorMessage, "GetAllEntitiesByUserIdAsyncCounter"));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
