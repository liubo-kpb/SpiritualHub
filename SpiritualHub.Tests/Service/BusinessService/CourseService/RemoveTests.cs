namespace SpiritualHub.Tests.Service.BusinessService.CourseService;

using Moq;
using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class RemoveTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var user = _users.First();
        var course = GetCourseWithStudent(user);

        var courseId = course.Id.ToString();
        var userId = course.Students.First().Id.ToString();

        int expectedStudentsCount = course.Students.Count - 1;

        _courseRepositoryMock.Setup(x => x.GetCourseWithStudentsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        await _courseService.RemoveAsync(courseId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.Students, Has.Count.EqualTo(expectedStudentsCount));
            Assert.That(course.Students.Any(u => u.Id == user.Id), Is.False);
        });
        _courseRepositoryMock.Verify(x => x.GetCourseWithStudentsAsync(It.Is<string>(x => x == courseId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _userRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenUserDoesNotExist()
    {
        // Arrange
        var user = _users.First();
        var course = GetCourseWithStudent(user);

        var courseId = course.Id.ToString();
        var userId = "wrongId";

        int expectedStudentsCount = course.Students.Count;

        _courseRepositoryMock.Setup(x => x.GetCourseWithStudentsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        // Act
        await _courseService.RemoveAsync(courseId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(course.Students, Has.Count.EqualTo(expectedStudentsCount));
        });
        _courseRepositoryMock.Verify(x => x.GetCourseWithStudentsAsync(It.Is<string>(x => x == courseId)));
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
        _userRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenCourseDoesNotExist_ThrowTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var courseId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.RemoveAsync(courseId, userId), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WhenCourseDoesNotExist_MethodCallTest()
    {
        // Arrange
        var user = new ApplicationUser();

        var courseId = "wrongId";
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        try
        {
            await _courseService.RemoveAsync(courseId, userId);
        }
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetCourseWithStudentsAsync(It.Is<string>(x => x == courseId)));
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)));
            _userRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {

        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
