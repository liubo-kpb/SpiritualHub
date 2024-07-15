namespace SpiritualHub.Tests.Controller.ProductController.PostMethods;

using Microsoft.AspNetCore.Mvc;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class RemoveTests : MockConfiguration
{
    [Test]
    public async Task Remove_WhenSuccess_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";

        // Act
        var result = await Controller.Remove(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Mine)));
            AssertTempData(SuccessMessage, Controller.RemovedEntityMessage);
            AssertCounters(1, 1, 1);
        });
    }

    [Test]
    public async Task Remove_WhenEntityDoesNotExist_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ExistsFlag = false;

        // Act
        var result = await Controller.Remove(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.All)));
            AssertTempData(ErrorMessage, string.Format(NoEntityFoundErrorMessage, EntityName));
            AssertCounters(1, 0, 0);
        });
    }

    [Test]
    public async Task Remove_WhenRemoveAsyncThrowsNotImplementedException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Remove(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Index"));
            AssertTempData(ErrorMessage, TestErrorMessageForExceptions);
            AssertCounters(1, 1, 0);
        });
    }

    [Test]
    public async Task Remove_WhenRemoveAsyncThrowsException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Remove(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Details)));
            AssertTempData(ErrorMessage, string.Format(GeneralUnexpectedErrorMessage, $"remove {EntityName} from your space"));
            AssertCounters(1, 1, 0);
        });
    }

    private void AssertTempData(string key, string expectedMessage)
    {
        Assert.That(Controller.TempData[key], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, $"{nameof(Controller.TempData)}[{key}]"));
    }

    private void AssertCounters(int expectedExistsCounter, int expectedRemoveCount, int expectedRemoveMessageCount)
    {
        Assert.That(Controller.ExistsCounter, Is.EqualTo(expectedExistsCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.ExistsCounter)));
        Assert.That(Controller.RemoveAsyncCounter, Is.EqualTo(expectedRemoveCount), string.Format(WrongVariableValueErrorMessage, nameof(Controller.RemoveAsyncCounter)));
        Assert.That(Controller.RemoveEntitySuccessMessageCounter, Is.EqualTo(expectedRemoveMessageCount), string.Format(WrongVariableValueErrorMessage, nameof(Controller.RemoveEntitySuccessMessageCounter)));
    }
}
