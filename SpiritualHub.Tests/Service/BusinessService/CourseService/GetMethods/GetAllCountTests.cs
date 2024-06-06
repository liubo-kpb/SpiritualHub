namespace SpiritualHub.Tests.Service.BusinessService.CourseService.GetMethods;

using MockQueryable.Moq;

public class GetAllCountTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        int expeceted = _courses.Count;

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        // Act
        var result = await _courseService.GetAllCountAsync();

        // Assert
        Assert.That(result, Is.EqualTo(expeceted));
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking());
    }
}
