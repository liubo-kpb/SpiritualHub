namespace SpiritualHub.Tests.Service.BusinessService.CourseService.CRUDMethods;

using Moq;

using Data.Models;

using static Extensions.Common.TestErrorMessagesConstants;

public class DeleteTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var course = new Course()
        {
            Image = new Image()
        };
        var courseId = course.Id.ToString();

        _courseRepositoryMock.Setup(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId))).ReturnsAsync(course);

        // Act
        await _courseService.DeleteAsync(courseId);

        // Assert
        _courseRepositoryMock.Verify(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId)), Times.Once);
        _courseRepositoryMock.Verify(x => x.Delete(It.Is<Course>(x => x.Equals(course))));

        _imageRepositoryMock.Verify(x => x.Delete(It.Is<Image>(x => x.Equals(course.Image))));
        _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.Is<ICollection<Rating>>(x => x.Equals(course.Ratings))));


        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public void WithWrongId_ThrowTest()
    {
        // Arrange
        var courseId = "wrongId";

        // Act & Assert
        Assert.ThrowsAsync<NullReferenceException>(async () => await _courseService.DeleteAsync(courseId), NoNullReferenceExceptionErrorMessage);
    }

    [Test]
    public async Task WithWrongId_MethodCallTest()
    {
        // Arrange
        var courseId = "wrongId";

        // Act
        try
        {
            await _courseService.DeleteAsync(courseId);
        }
        // Assert
        catch (NullReferenceException)
        {
            _courseRepositoryMock.Verify(x => x.GetCourseWithModulesImageAndRatingsAsync(It.Is<string>(x => x == courseId)), Times.Once);
            _courseRepositoryMock.Verify(x => x.Delete(It.IsAny<Course>()));

            _imageRepositoryMock.Verify(x => x.Delete(It.IsAny<Image>()), Times.Never);
            _ratingRepositoryMock.Verify(x => x.DeleteMultiple(It.IsAny<ICollection<Rating>>()), Times.Never);


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
}
