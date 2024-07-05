namespace SpiritualHub.Tests.Controller.BaseController.GetMethods;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Category;

using static Extensions.Common.TestMessageConstants;

using static Common.NotificationMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;

[TestFixture]
public class AllTests : MockConfiguration
{
    private const int EXPECTED_CALL_COUNT = 1;

    [Test]
    public async Task All_ReturnsViewResult()
    {
        // Arrange
        var queryModel = new BaseQueryModel<EmptyViewModel, Enum>();
        var categories = new List<CategoryServiceModel> { new CategoryServiceModel { Name = "Category1" }, new CategoryServiceModel { Name = "Category2" } };
        _categoryServiceMock.Setup(c => c.GetAllAsync(It.Is<string>(x => x == null))).ReturnsAsync(categories);

        // Act
        var result = await Controller.All(queryModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(Controller.GetAllAsyncCounter, Is.EqualTo(EXPECTED_CALL_COUNT));
        });
    }

    [Test]
    public async Task All_SetsCategoriesInViewData()
    {
        // Arrange
        var queryModel = new BaseQueryModel<EmptyViewModel, Enum>();
        var categories = new List<CategoryServiceModel> { new CategoryServiceModel { Name = "Category1" }, new CategoryServiceModel { Name = "Category2" } };
        _categoryServiceMock.Setup(c => c.GetAllAsync(It.Is<string>(x => x == null))).ReturnsAsync(categories);

        // Act
        var result = await Controller.All(queryModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ViewResult>());
            Assert.That(Controller.GetAllAsyncCounter, Is.EqualTo(EXPECTED_CALL_COUNT));

            var model = ((ViewResult) result).ViewData.Model as BaseQueryModel<EmptyViewModel, Enum>;
            Assert.That(model!.Categories, Is.EqualTo(categories.Select(c => c.Name)));
        });
        _categoryServiceMock.Verify(c => c.GetAllAsync(It.Is<string>(x => x == null)));
    }

    [Test]
    public async Task All_RedirectsToHomeControllerIndexOnException()
    {
        // Arrange
        var queryModel = new BaseQueryModel<EmptyViewModel, Enum>();
        var expectedErrorMessage = string.Format(GeneralUnexpectedErrorMessage, $"load {EntityName}s");

        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.All(queryModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(Controller.GetAllAsyncCounter, Is.EqualTo(EXPECTED_CALL_COUNT));
            Assert.That(Controller.ThrowExceptionCounter, Is.EqualTo(EXPECTED_CALL_COUNT));
            Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedErrorMessage));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    [Test]
    public async Task All_ReturnsViewResultOnNotImplementedException()
    {
        // Arrange
        var queryModel = new BaseQueryModel<EmptyViewModel, Enum>();
        var expectedErrorMessage = TestErrorMessageForExceptions;

        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.All(queryModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(Controller.GetAllAsyncCounter, Is.EqualTo(EXPECTED_CALL_COUNT));
            Assert.That(Controller.ThrowNotImplementedExceptionCounter, Is.EqualTo(EXPECTED_CALL_COUNT));
            Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedErrorMessage));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }
}
