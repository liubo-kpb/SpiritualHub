namespace SpiritualHub.Tests.Service.BusinessService.CourseService;

using Moq;

using static Extensions.Common.TestErrorMessagesConstants;

public class GetCourseTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var course = _courses.First();
        var courseId = course.Id.ToString();
        var user = _users.First();
        var userId = user.Id.ToString();

        _courseRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        await _courseService.GetAsync(courseId, userId);

        // Assert
        Assert.That(course.Students.Any(s => s.Id == user.Id));
        _courseRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == courseId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenFail_WrongUserId()
    {
        // Arrange
        var course = _courses.First();
        var courseId = course.Id.ToString();
        var userId = "wrongId";

        _courseRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        // Act
        await _courseService.GetAsync(courseId, userId);

        // Assert
        Assert.That(course.Students.Any(s => s != null && s.Id.ToString() == userId), Is.False);
        _courseRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == courseId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WhenFail_WrongCourseId_ThrowTest()
    {
        // Arrange
        var courseId = "wrongId";
        var userId = "userId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.GetAsync(courseId, userId));
    }

    [Test]
    public async Task WhenFaile_WrongCourseId_MethodCallTest()
    {
        // Arrange
        var courseId = "wrongId";
        var user = _users.First();
        var userId = user.Id.ToString();

        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(user);

        // Act
        try
        {
            await _courseService.GetAsync(courseId, userId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == courseId)), Times.Once);
            _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
            _courseRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);

            return;
        }
        catch (Exception)
        {
        }

        Assert.Fail(NoNullReferenceExceptionErrorMessage);
    }
}
