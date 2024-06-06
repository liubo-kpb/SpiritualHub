namespace SpiritualHub.Tests.Service.BusinessService.CourseService.GetMethods;

using Moq;

using Client.ViewModels.Course;
using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class GetCourseDetailsTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var course = GetCourseWithStudent();
        course.Modules = new List<Module>()
        {
            new Module()
            {
                Number = 2,
            },
            new Module()
            {
                Number = 3,
            },
            new Module()
            {
                Number = 1,
            },
        };
        var userId = course.Students.First().Id.ToString();
        var courseId = course.Id.ToString();

        _courseRepositoryMock.Setup(x => x.GetCourseDetailsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        var expected = _mapper.Map<CourseDetailsViewModel>(course);
        expected.UserHasCourse = true;

        // Act
        var result = await _courseService.GetCourseDetailsAsync(courseId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.UserHasCourse, Is.True);

            int expectedModuleNumber = 1;
            foreach (var module in result.Modules)
            {
                Assert.That(module.Number, Is.EqualTo(expectedModuleNumber++));
            }
        });
        _courseRepositoryMock.Verify(x => x.GetCourseDetailsAsync(It.Is<string>(x => x == courseId)), Times.Once);
    }

    [Test]
    public async Task WhenSuccess_UserDoesNotHaveCourse()
    {
        // Arrange
        var course = GetCourseWithStudent();
        course.Modules = new List<Module>()
        {
            new Module()
            {
                Number = 2,
            },
            new Module()
            {
                Number = 3,
            },
            new Module()
            {
                Number = 1,
            },
        };
        var userId = "userId";
        var courseId = course.Id.ToString();

        _courseRepositoryMock.Setup(x => x.GetCourseDetailsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        var expected = _mapper.Map<CourseDetailsViewModel>(course);
        expected.UserHasCourse = true;

        // Act
        var result = await _courseService.GetCourseDetailsAsync(courseId, userId);

        // Assert
        Assert.That(result.UserHasCourse, Is.False);
        _courseRepositoryMock.Verify(x => x.GetCourseDetailsAsync(It.Is<string>(x => x == courseId)), Times.Once);
    }

    [Test]
    public void WhenFail_WrongCourseId_ThrowTest()
    {
        // Arrange
        var courseId = "wrongId";
        var userId = "userId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.GetCourseDetailsAsync(courseId, userId));
    }

    [Test]
    public async Task WhenFail_WrongCourseId_MethodCallTest()
    {
        // Arrange
        var courseId = "wrongId";
        var userId = "userId";

        // Act
        try
        {
            var result = await _courseService.GetCourseDetailsAsync(courseId, userId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetCourseDetailsAsync(It.Is<string>(x => x == courseId)), Times.Once);

            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
