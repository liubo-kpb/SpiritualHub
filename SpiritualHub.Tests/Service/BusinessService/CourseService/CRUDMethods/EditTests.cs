namespace SpiritualHub.Tests.Service.BusinessService.CourseService.CRUDMethods;

using Moq;

using Client.ViewModels.Course;
using Client.ViewModels.Module;
using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class EditTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var courseEntity = GetCourseEntity();
        var courseFormModel = GetCourseFormModel(courseEntity.Id);
        var expected = _mapper.Map<Course>(courseFormModel);

        _courseRepositoryMock.Setup(x => x.GetCourseInfoAsync(It.Is<string>(x => x == courseFormModel.Id))).ReturnsAsync(courseEntity);

        // Act
        await _courseService.EditAsync(courseFormModel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(courseEntity, Is.EqualTo(expected));
            Assert.That(courseEntity.Image.URL, Is.EqualTo(expected.Image.URL));
            Assert.That(courseEntity.CategoryID, Is.EqualTo(expected.CategoryID));
            Assert.That(courseEntity.AuthorID, Is.EqualTo(expected.AuthorID));
            Assert.That(courseEntity.PublisherID, Is.EqualTo(expected.PublisherID));
        });
        _courseRepositoryMock.Verify(x => x.GetCourseInfoAsync(It.Is<string>(x => x == courseFormModel.Id)));

        _moduleServiceMock.Verify(x => x.DeleteModules(It.IsAny<ICollection<Module>>(), It.IsAny<IEnumerable<CourseModuleFormModel>>()), Times.Never);
        _moduleServiceMock.Verify(x => x.Edit(It.IsAny<Module>(), It.IsAny<CourseModuleFormModel>()), Times.Never);
        _moduleServiceMock.Verify(x => x.ReorderCourseModules(It.IsAny<ICollection<Module>>(), It.IsAny<int>()), Times.Once);

        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenSuccess_WithModules()
    {
        // Arrange
        var courseEntity = GetCourseEntity();
        var courseFormModel = GetCourseFormModel(courseEntity.Id);
        courseFormModel.Modules = new List<CourseModuleFormModel>()
        {
            new CourseModuleFormModel()
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = true,
            },
            new CourseModuleFormModel()
            {
                Id = Guid.NewGuid().ToString(),
            },
            new CourseModuleFormModel()
            {
                IsNew = true,
            },
        };

        foreach (var module in courseFormModel.Modules.Where(m => !m.IsNew))
        {
            courseEntity.Modules.Add(_mapper.Map<Module>(module));
        }

        _courseRepositoryMock.Setup(x => x.GetCourseInfoAsync(It.Is<string>(x => x == courseFormModel.Id))).ReturnsAsync(courseEntity);
        _moduleServiceMock.Setup(x => x.DeleteModules(It.IsAny<ICollection<Module>>(), It.IsAny<IEnumerable<CourseModuleFormModel>>())).Returns(new List<Module>());

        // Act
        await _courseService.EditAsync(courseFormModel);

        // Assert
        _courseRepositoryMock.Verify(x => x.GetCourseInfoAsync(It.Is<string>(x => x == courseFormModel.Id)));

        _moduleServiceMock.Verify(x => x.DeleteModules(It.IsAny<ICollection<Module>>(), It.IsAny<IEnumerable<CourseModuleFormModel>>()), Times.Once);
        _moduleServiceMock.Verify(x => x.Edit(It.IsAny<Module>(), It.IsAny<CourseModuleFormModel>()), Times.Once);
        _moduleServiceMock.Verify(x => x.ReorderCourseModules(It.IsAny<ICollection<Module>>(), It.IsAny<int>()), Times.Once);

        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WithWrongId_ThrowTest()
    {
        // Arrange
        var courseFormModel = new CourseFormModel()
        {
            Id = "wrongId",
        };

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.EditAsync(courseFormModel), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WithWrongId_MethodCallTest()
    {
        // Arrange
        var courseFormModel = new CourseFormModel()
        {
            Id = "wrongId",
        };

        // Act
        try
        {
            await _courseService.EditAsync(courseFormModel);
        }
        // Assert
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetCourseInfoAsync(It.Is<string>(x => x == courseFormModel.Id)), Times.Once);

            _moduleServiceMock.Verify(x => x.DeleteModules(It.IsAny<ICollection<Module>>(), It.IsAny<IEnumerable<CourseModuleFormModel>>()), Times.Never);
            _moduleServiceMock.Verify(x => x.Edit(It.IsAny<Module>(), It.IsAny<CourseModuleFormModel>()), Times.Never);
            _moduleServiceMock.Verify(x => x.ReorderCourseModules(It.IsAny<ICollection<Module>>(), It.IsAny<int>()), Times.Never);

            _courseRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }

    public override void OneTimeSetup()
    {
        GenerateEntities = false;
        base.OneTimeSetup();
    }

    private static CourseFormModel GetCourseFormModel(Guid courseId)
    {
        return new CourseFormModel()
        {
            Id = courseId.ToString(),
            Name = "Test Name",
            Description = "Test Description",
            ShortDescription = "Test ShortDescription",
            Price = 123.4m,
            ImageUrl = "new url",
            IsActive = true,
            CategoryId = 1,
            AuthorId = Guid.NewGuid().ToString(),
            PublisherId = Guid.NewGuid().ToString(),
        };
    }

    private Course GetCourseEntity()
    {
        return new Course()
        {
            Name = "Old Name",
            Description = "Old Description",
            ShortDescription = "Old ShortDescription",
            Price = 123,
            Image = new Image { URL = "old url" },
            IsActive = false,
            CategoryID = 2,
            AuthorID = Guid.NewGuid(),
            PublisherID = Guid.NewGuid(),
            Modules = new List<Module>(),
        };
    }
}
