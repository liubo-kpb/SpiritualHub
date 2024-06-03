namespace SpiritualHub.Tests.Service.BusinessService.CourseService.GetMethods;

using MockQueryable.Moq;

using Client.ViewModels.Course;

public class AllCoursesByUserIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasCourses_OneIsInactive()
    {
        // Arrange
        var user = _users.First();
        var userId = user.Id.ToString();

        _courses[0].Students.Add(user);
        _courses[1].Students.Add(user);
        _courses[1].IsActive = false;

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        var expected = new List<CourseViewModel>()
        {
            _mapper.Map<CourseViewModel>(_courses[0]),
            _mapper.Map<CourseViewModel>(_courses[1]),
        };

        // Act
        var result = await _courseService.AllCoursesByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    [Test]
    public async Task WhenHasNooCourses()
    {
        // Arrange
        var userId = _users.First().Id.ToString();

        var expected = new List<CourseViewModel>();

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        // Act
        var result = await _courseService.AllCoursesByUserIdAsync(userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking());
    }
}
