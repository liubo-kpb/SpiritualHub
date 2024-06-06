namespace SpiritualHub.Tests.Service.BusinessService.CourseService;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class ShowTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var course = new Course()
        {
            IsActive = true,
            Modules = new List<Module>()
            {
                new Module()
                {
                    IsActive = true,
                    Number = 2,
                },
                new Module()
                {
                    IsActive = true,
                    Number = 3,
                },
                new Module()
                {
                    IsActive = true,
                    Number = 1,
                },
            }
        };

        var courseId = course.Id.ToString();

        _courseRepositoryMock.Setup(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        // Act
        await _courseService.ShowAsync(courseId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.IsActive, Is.True);

            foreach (var module in course.Modules)
            {
                Assert.That(module.IsActive, Is.True);
            }

        });
        _courseRepositoryMock.Verify(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId)), Times.Once);
        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenWrongId_ThrowTest()
    {
        // Arrange
        var courseId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.ShowAsync(courseId));
    }

    [Test]
    public async Task WhenWrongId_MethodCallTest()
    {
        // Arrange
        var courseId = "wrongId";

        // Act
        try
        {
            await _courseService.ShowAsync(courseId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId)), Times.Once);
            _courseRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
