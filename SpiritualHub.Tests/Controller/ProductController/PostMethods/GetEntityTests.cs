namespace SpiritualHub.Tests.Controller.ProductController.PostMethods;

using Microsoft.AspNetCore.Mvc;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Extensions.Common.TestMessageConstants;
using static Extensions.Common.TestErrorMessagesConstants;

public class GetEntityTests : MockConfiguration
{
    [Test]
    public async Task Get_WhenSuccess_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Mine)));
            AssertTempData(SuccessMessage, Controller.GotEntityMessage);
            AssertCounters(1, 1, 0, 1, 1);
        });
    }

    [Test]
    public async Task Get_WhenSuccess_UserIsAdmin_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.IsAdmin = true;

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Mine)));
            AssertTempData(SuccessMessage, Controller.GotEntityMessage);
            AssertCounters(1, 0, 0, 1, 1);
        });
    }

    [Test]
    public async Task Get_WhenEntityExistsAndHasEntity_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.HasEntityAsyncFlag = true;

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Details)));
            AssertTempData(ErrorMessage, Controller.AlreadyHasEntityMessage);
            AssertCounters(1, 1, 1, 0, 0);
        });
    }

    [Test]
    public async Task Get_WhenEntityDoesNotExist_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ExistsFlag = false;

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.All)));
            AssertTempData(ErrorMessage, string.Format(NoEntityFoundErrorMessage, EntityName));
            AssertCounters(1, 0, 0, 0, 0);
        });
    }

    [Test]
    public async Task Get_WhenGetAsyncThrowsNotImplementedException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo("Index"));
            AssertTempData(ErrorMessage, TestErrorMessageForExceptions);
            AssertCounters(1, 1, 0, 1, 0);
        });
    }

    [Test]
    public async Task Get_WhenGetAsyncThrowsException_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "id";
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Get(id) as RedirectToActionResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ActionName, Is.EqualTo(nameof(Controller.Details)));
            AssertTempData(ErrorMessage, string.Format(GeneralUnexpectedErrorMessage, $"get {EntityName}"));
            AssertCounters(1, 1, 0, 1, 0);
        });
    }

    private void AssertTempData(string key, string expectedMessage)
    {
        Assert.That(Controller.TempData[key], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, $"{nameof(Controller.TempData)}[{key}]"));
    }

    private void AssertCounters(int expectedExistsCounter, int expectedHasEntityCounter, int expectedAlreadyHasEntityCounter, int expectedGetCounter, int expectedGetEntityMessageCounter)
    {
        Assert.That(Controller.ExistsCounter, Is.EqualTo(expectedExistsCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.ExistsCounter)));
        Assert.That(Controller.HasEntityAsyncCounter, Is.EqualTo(expectedHasEntityCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.HasEntityAsyncCounter)));
        Assert.That(Controller.AlreadyHasEntityCounter, Is.EqualTo(expectedAlreadyHasEntityCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.AlreadyHasEntityCounter)));
        Assert.That(Controller.GetAsyncCounter, Is.EqualTo(expectedGetCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.GetAsyncCounter)));
        Assert.That(Controller.GetEntitySuccessMessageCounter, Is.EqualTo(expectedGetEntityMessageCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.GetEntitySuccessMessageCounter)));
    }
}
