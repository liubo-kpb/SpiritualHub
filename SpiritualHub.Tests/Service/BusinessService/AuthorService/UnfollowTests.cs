namespace SpiritualHub.Tests.Service.BusinessService.AuthorService;

using Moq;

public class UnfollowTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithFollower(testUser);

        string userId = testUser.Id.ToString();
        string authorId = testAuthor.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithFollowersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(testUser);

        int expectedAuthorFollowerCount = testAuthor.Followers.Count - 1;

        // Act
        await _authorService.UnfollowAsync(authorId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Followers.All(f => f.Id != testUser.Id), "User is still following the author.");
            Assert.That(testAuthor.Followers, Has.Count.EqualTo(expectedAuthorFollowerCount), "Expected followers count does not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithFollowersAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync());
    }

    [Test]
    public async Task WhenNotFollowing()
    {
        // Arrange
        var testUser = _users.First();
        var testFollower = _users[2];
        var testAuthor = GetAuthorWithFollower(testFollower);

        string userId = testUser.Id.ToString();
        string authorId = testAuthor.Id.ToString();

        _authorRepositoryMock.Setup(x => x.GetAuthorWithFollowersAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userRepositoryMock.Setup(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId))).ReturnsAsync(testUser);

        int expectedAuthorFollowerCount = testAuthor.Followers.Count;

        // Act
        await _authorService.UnfollowAsync(authorId, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testAuthor.Followers.All(f => f.Id != testUser.Id), "User is following the author when it shouldn't be.");
            Assert.That(testAuthor.Followers, Has.Count.EqualTo(expectedAuthorFollowerCount), "Expected followers count does not match.");
            Assert.That(testAuthor.Followers.Any(f => f.Id == testFollower.Id), "The wrong user was removed from the followers.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithFollowersAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userRepositoryMock.Verify(x => x.GetSingleByIdAsync(It.Is<string>(x => x == userId)), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
