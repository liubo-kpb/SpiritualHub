namespace SpiritualHub.Tests.Service.BusinessService.CourseService.CRUDMethods;

using Moq;
using AutoMapper;

using Data.Models;
using Client.ViewModels.Course;

public class CreateTests : MockConfiguration
{
    private Mock<IMapper> _mapperMock = null!;

    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var newCourse = new CourseFormModel()
        {
            Name = "Test",
            Description = "Test Description",
            ShortDescription = "Test Short Description",
            Price = 123,
            ImageUrl = "Test URL",
            IsActive = true,
        };
        var courseEntity = new Course()
        {
            Name = newCourse.Name,
            Description = newCourse.Description,
            ShortDescription = newCourse.ShortDescription,
            Price = newCourse.Price,
            Image = new Image() { URL = newCourse.ImageUrl },
            IsActive = newCourse.IsActive,

        };

        _mapperMock.Setup(x => x.Map<Course>(It.Is<CourseFormModel>(x => x.Equals(newCourse)))).Returns(courseEntity);

        // Act
        var result = await _courseService.CreateAsync(newCourse);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(courseEntity.Id.ToString()));
            Assert.That(courseEntity.Image.Name, Is.EqualTo(courseEntity.Name));
        });
        _mapperMock.Verify(x => x.Map<Course>(It.Is<CourseFormModel>(x => x.Equals(newCourse))), Times.Once);
        _courseRepositoryMock.Verify(x => x.AddAsync(It.Is<Course>(x => x.Equals(courseEntity))), Times.Once);
        _courseRepositoryMock.Verify(x => x.SaveChangesAsync());
        _moduleServiceMock.Verify(x => x.ReorderCourseModules(It.IsAny<ICollection<Module>>(), It.IsAny<int>()));
    }

    public override void OneTimeSetup()
    {
        _mapperMock = new Mock<IMapper>();
        _mapper = _mapperMock.Object;

        GenerateEntities = false;
    }
}
