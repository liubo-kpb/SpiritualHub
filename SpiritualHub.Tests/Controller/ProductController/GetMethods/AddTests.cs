namespace SpiritualHub.Tests.Controller.ProductController.GetMethods;

using Microsoft.AspNetCore.Mvc;
using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Category;
using Client.ViewModels.Publisher;
using Client.ViewModels.Author;
using Data.Models;

using static Common.NotificationMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Extensions.Common.TestErrorMessagesConstants;
using static Extensions.Common.TestMessageConstants;

public class AddTests : MockConfiguration
{
    [Test]
    public async Task Add_WhenSuccess_UserIsAdmin()
    {
        // Arrange
        Controller.IsAdmin = true;
        IEnumerable<PublisherInfoViewModel> publishers = new List<PublisherInfoViewModel>
        {
            new PublisherInfoViewModel()
        };
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };
        IEnumerable<AuthorInfoViewModel> authors = new List<AuthorInfoViewModel>
        {
            new AuthorInfoViewModel(),
        };

        _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);
        _authorServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(authors);

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var modeulResult = ((ViewResult) result).Model as ProductFormModel;
            Assert.That(modeulResult!.Publishers, Is.EqualTo(publishers));
            Assert.That(modeulResult!.Categories, Is.EqualTo(categories));
            Assert.That(modeulResult!.Authors, Is.EqualTo(authors));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        _publisherServiceMock.Verify(x => x.GetConnectedAuthorsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
        _authorServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        _authorServiceMock.Verify(x => x.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Add_WhenSuccess_UserIsPublisher()
    {
        // Arrange
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };
        IEnumerable<AuthorInfoViewModel> authors = new List<AuthorInfoViewModel>
        {
            new AuthorInfoViewModel(),
        };

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.GetConnectedAuthorsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(authors);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var modeulResult = ((ViewResult) result).Model as ProductFormModel;
            Assert.That(modeulResult!.Categories, Is.EqualTo(categories));
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _publisherServiceMock.Verify(x => x.GetConnectedAuthorsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
        _authorServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _authorServiceMock.Verify(x => x.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Add_WhenUserIsPublisher_HasNoAuthors_ReturnsRedirectToAction()
    {
        // Arrange
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };
        IEnumerable<AuthorInfoViewModel> authors = new List<AuthorInfoViewModel>();

        _publisherServiceMock.Setup(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(true);
        _publisherServiceMock.Setup(x => x.GetConnectedAuthorsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(authors);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            
            var redirectResult = (RedirectToActionResult) result;
            Assert.That(redirectResult.ActionName, Is.EqualTo("All"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo(nameof(Author)));
            AssertTempData(NoConnectedAuthorsErrorMessage);
            AssertCounters(1);
        });
        _publisherServiceMock.Verify(x => x.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _publisherServiceMock.Verify(x => x.GetConnectedAuthorsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
        _authorServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _authorServiceMock.Verify(x => x.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Add_WhenUserIsNotAPublisher_ReturnsRedirectToAction()
    {
        // Arrange
        _publisherServiceMock.Setup(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(false);

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(0);
            AssertTempData(NotAPublisherErrorMessage);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Become"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Publisher"));
        });
        _publisherServiceMock.Verify(p => p.ExistsByUserIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);

        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _publisherServiceMock.Verify(x => x.GetConnectedAuthorsByUserIdAsync(It.IsAny<string>()), Times.Never);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Never);
        _authorServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        _authorServiceMock.Verify(x => x.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Add_WhenNotImplementedExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.IsAdmin = true;
        Controller.ThrowNotImplementedExceptionFlag = true;

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(Controller.ThrowNotImplementedExceptionCounter, Is.EqualTo(1));
            AssertTempData(TestErrorMessageForExceptions);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    [Test]
    public async Task Add_WhenExceptionThrown_ReturnsRedirectToAction()
    {
        // Arrange
        Controller.IsAdmin = true;
        Controller.ThrowExceptionFlag = true;

        // Act
        var result = await Controller.Add();

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounters(1);
            Assert.That(Controller.ThrowExceptionCounter, Is.EqualTo(1));
            AssertTempData(string.Format(GeneralUnexpectedErrorMessage, "load page"));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            Assert.That(((RedirectToActionResult) result).ActionName, Is.EqualTo("Index"));
            Assert.That(((RedirectToActionResult) result).ControllerName, Is.EqualTo("Home"));
        });
    }

    private void AssertCounters(int expectedCreateFormModelCounter)
    {
        Assert.That(Controller.CreateFormModelCounter, Is.EqualTo(expectedCreateFormModelCounter), string.Format(WrongVariableValueErrorMessage, nameof(Controller.CreateFormModelCounter)));
    }

    private void AssertTempData(string expectedMessage)
    {
        Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(expectedMessage), string.Format(WrongVariableValueErrorMessage, "TempData[ErrorMessage]"));
    }
}
