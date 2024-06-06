namespace SpiritualHub.Tests.Service.BusinessService.CourseService.GetMethods;

using Moq;
using MockQueryable.Moq;

using Client.ViewModels.Course;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;

public class GetCoursesByPublisherIdTests : MockConfiguration
{
    [Test]
    public async Task WhenHasCourses()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();
        _courses[0].PublisherID = publisher.Id;
        _courses[1].PublisherID = publisher.Id;

        _courses[0].Students.Add(_users.First(u => u.Id == publisher.UserID));

        var courses = new List<Course>()
        {
            _courses[0],
            _courses[1],
            _courses[3]
        };

        var expected = new List<CourseViewModel>();
        _mapper.MapListToViewModel(courses, expected);

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        // Act
        var result = await _courseService.GetCoursesByPublisherIdAsync(publisher.Id.ToString(), publisher.UserID.ToString());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));

            var courseWithReader = result.First(b => b.Id == _courses[0].Id.ToString());
            Assert.That(courseWithReader.UserHasCourse, Is.True);
        });
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    [Test]
    public async Task WhenHasNoCourses()
    {
        // Arrange
        var publisher = new SeedPublisherConfiguration().GenerateEntities().First();

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(new List<Course>().AsQueryable().BuildMock());

        // Act
        var result = await _courseService.GetCoursesByPublisherIdAsync(publisher.Id.ToString(), publisher.UserID.ToString());

        // Assert
        Assert.That(result.Count(), Is.EqualTo(0));
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }
}
