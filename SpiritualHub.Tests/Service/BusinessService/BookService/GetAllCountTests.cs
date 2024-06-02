namespace SpiritualHub.Tests.Service.BusinessService.BookService;

using MockQueryable.Moq;

public class GetAllCountTests : MockConfiguration
{
    [Test]
    public async Task Success()
    {
        // Arrange
        int expeceted = _books.Count;

        _bookRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_books.AsQueryable().BuildMock());

        // Act
        var result = await _bookService.GetAllCountAsync();

        // Assert
        Assert.That(result, Is.EqualTo(expeceted));
        _bookRepositoryMock.Verify(x => x.AllAsNoTracking());
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
