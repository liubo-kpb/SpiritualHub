namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using MockQueryable.Moq;

public class GetAllCountTests : MockConfiguration
{
    [Test]
    public async Task Success()
    {
        // Arrange
        int expeceted = _events.Count;

        _eventRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_events.AsQueryable().BuildMock());

        // Act
        var result = await _eventService.GetAllCountAsync();

        // Assert
        Assert.That(result, Is.EqualTo(expeceted));
        _eventRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
