namespace SpiritualHub.Tests.Controller.BaseController.PostMethods;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Client.ViewModels.BaseModels;
using Client.ViewModels.Category;
using Client.ViewModels.Publisher;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class AddTests : MockConfiguration
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Add_WhenSuccess(bool isAdmin)
    {
        // Arrange
        Controller.IsAdmin = isAdmin;
        string publisherId = "publisherId";
        var newEntityForm = new BaseFormModel()
        {
            CategoryId = 1,
            PublisherId = isAdmin ? publisherId : null,
        };
        IActionResult validationResult = null!;

        _validationServiceMock.Setup(x => x.CheckUserIsPublisherAsync()).ReturnsAsync(validationResult!);
        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId))).ReturnsAsync(true);

        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(publisherId);
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == publisherId))).ReturnsAsync(true);
        }

        // Act
        var result = await Controller.Add(newEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(1);
            Assert.That(Controller.TempData[SuccessMessage]!, Is.EqualTo(string.Format(CreationSuccessfulMessage, EntityName)));
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            var redirectToActionResult = (RedirectToActionResult) result;
            Assert.That(redirectToActionResult.ActionName!, Is.EqualTo("Details"));
            Assert.That(redirectToActionResult.RouteValues!["id"]!, Is.EqualTo(Controller.EntityId));
        });
        _validationServiceMock.Verify(x => x.CheckUserIsPublisherAsync(), isAdmin ? Times.Never : Times.Once);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Never);

        _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == newEntityForm.PublisherId)), Times.Once);
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Never);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
        }
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Add_InvalidFormModel_ReturnsViewResult(bool isAdmin)
    {
        // Arrange
        Controller.IsAdmin = isAdmin;
        var publisherId = "wrongId";
        var newEntityForm = new BaseFormModel()
        {
            CategoryId = -1,
            PublisherId = isAdmin ? publisherId : null,
        };

        IEnumerable<PublisherInfoViewModel> publishers = new List<PublisherInfoViewModel>
        {
            new PublisherInfoViewModel()
        };
        ICollection<CategoryServiceModel> categories = new List<CategoryServiceModel>
        {
            new CategoryServiceModel()
        };

        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId))).ReturnsAsync(false);
        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);
        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == publisherId))).ReturnsAsync(false);
            _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
        }

        // Act
        var result = await Controller.Add(newEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(0);

            Assert.That(Controller.ModelState.ErrorCount, Is.EqualTo(isAdmin ? 2 : 1));
            Assert.That(Controller.ModelState[nameof(newEntityForm.CategoryId)]!.Errors.First().ErrorMessage, Is.EqualTo(string.Format(NoEntityFoundErrorMessage, "category")));
            if (isAdmin)
            {
                Assert.That(Controller.ModelState[nameof(newEntityForm.PublisherId)]!.Errors.First().ErrorMessage, Is.EqualTo(string.Format(NoEntityFoundErrorMessage, "publisher")));
                Assert.That(Controller.ModelState[nameof(newEntityForm.PublisherId)]!.Errors, Has.Count.EqualTo(1));
            }
            else
            {
                Assert.That(Controller.ModelState.ContainsKey(nameof(newEntityForm.PublisherId)), Is.False);
            }
            Assert.That(Controller.ModelState[nameof(newEntityForm.CategoryId)]!.Errors, Has.Count.EqualTo(1));

            var resultModel = (BaseFormModel) ((ViewResult) result).Model!;
            Assert.That(resultModel.Categories, Is.EqualTo(categories));
            if (isAdmin)
            {
                Assert.That(resultModel.Publishers, Is.EqualTo(publishers));
            }
            else
            {
                Assert.That(resultModel.Publishers.Count(), Is.EqualTo(0));
            }

            Assert.That(result, Is.InstanceOf<ViewResult>());
        });

        _validationServiceMock.Verify(x => x.CheckUserIsPublisherAsync(), Times.Never);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId)), Times.Once);
        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);

        _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.IsAny<string>()), Times.Never);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == newEntityForm.PublisherId)), Times.Once);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
        }
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task Add_ExceptionThrown(bool isAdmin)
    {
        // Arrange
        Controller.ThrowExceptionFlag = true;
        Controller.IsAdmin = isAdmin;
        var publisherId = "publisherId";
        var newEntityForm = new BaseFormModel()
        {
            CategoryId = 1,
            PublisherId = isAdmin ? publisherId : null,
        };

        var categories = new List<CategoryServiceModel>();
        var publishers = new List<PublisherInfoViewModel>();

        _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<string>())).ReturnsAsync(categories);
        _categoryServiceMock.Setup(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId))).ReturnsAsync(true);
        if (isAdmin)
        {
            _publisherServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(publishers);
            _publisherServiceMock.Setup(x => x.ExistsByIdAsync(It.Is<string>(x => x == publisherId))).ReturnsAsync(true);
        }
        else
        {
            _publisherServiceMock.Setup(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId))).ReturnsAsync(publisherId);
        }

        // Act
        var result = await Controller.Add(newEntityForm);

        // Assert
        Assert.Multiple(() =>
        {
            AssertCounter(1);
            Assert.That(Controller.ThrowExceptionCounter, Is.EqualTo(1));
            Assert.That(Controller.TempData[ErrorMessage], Is.EqualTo(string.Format(GeneralUnexpectedErrorMessage, $"create {EntityName}")));
            Assert.That(Controller.TempData.ContainsKey(SuccessMessage), Is.False);

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var resultModel = (BaseFormModel) ((ViewResult) result).Model!;
            Assert.That(resultModel.Categories, Is.EqualTo(categories));
            Assert.That(resultModel.Publishers, Is.EqualTo(publishers));
        });

        _categoryServiceMock.Verify(x => x.GetAllAsync(It.IsAny<string>()), Times.Once);
        _categoryServiceMock.Verify(x => x.ExistsAsync(It.Is<int>(x => x == newEntityForm.CategoryId)), Times.Once);
        if (isAdmin)
        {
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.IsAny<string>()), Times.Never);
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.Is<string>(x => x == newEntityForm.PublisherId)), Times.Once);
        }
        else
        {
            _publisherServiceMock.Verify(x => x.GetPublisherIdAsync(It.Is<string>(x => x == Controller.UserId)), Times.Once);
            _publisherServiceMock.Verify(x => x.GetAllAsync(), Times.Never);
            _publisherServiceMock.Verify(x => x.ExistsByIdAsync(It.IsAny<string>()), Times.Never);
        }
    }

    private void AssertCounter(int expectedCreateCallCount)
    {
        Assert.That(Controller.CreateAsyncCounter, Is.EqualTo(expectedCreateCallCount));
    }
}
