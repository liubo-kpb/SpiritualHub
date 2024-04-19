namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.GetMethods;

using Moq;

using Client.ViewModels.Author;
using Data.Models;

using static Common.GeneralApplicationConstants;

public class GetAuthorDetailsTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_UserIsAdmin_UserFollowsAuthor()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithFollower(testUser);
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        testAuthor.Publishers.Add(_publishers.First(p => p.UserID == testUser.Id));

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(true);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = true,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Once);
    }

    [Test]
    public async Task WhenSuccess_UserFollowsAuthor()
    {
        // Arrange
        var testUser = _users.First();
        var testAuthor = GetAuthorWithFollower(testUser);
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = true,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }

    [Test]
    public async Task WhenSuccess_NoFollowing()
    {
        // Arrange
        var testUser = _users[3];
        var testAuthor = GetAuthorWithFollower();
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = false,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }

    [Test]
    public async Task WhenSuccess_SubscribedToAuthor()
    {
        // Arrange
        var testUser = _users[2];
        var testAuthor = GetAuthorWithSubscriber(testUser);
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            SubscriberCount = 1,
            IsUserSubscribed = true,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }

    [Test]
    public async Task WhenSuccess_WhenSubscribedAndFollowing()
    {
        // Arrange
        var testUser = _users[2];
        var testAuthor = GetAuthorWithSubscriber(testUser);
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        testAuthor.Followers.Add(testUser);

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = true,
            SubscriberCount = 1,
            IsUserSubscribed = true,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }

    [Test]
    public async Task WhenSuccess_SubscribedAndNotFollowing_AuthorHasFollower()
    {
        // Arrange
        var testUser = _users[2];
        var testAuthor = GetAuthorWithSubscriber(testUser);
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        testAuthor.Followers.Add(_users[0]);

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = false,
            SubscriberCount = 1,
            IsUserSubscribed = true,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }

    [Test]
    public async Task WhenSuccess_FollowingAndNotSubscribed_AuthorHasSubscriber()
    {
        // Arrange
        var testUser = _users[2];
        var testAuthor = GetAuthorWithSubscriber(_users.First());
        var authorId = testAuthor.Id.ToString();
        var userId = testUser.Id.ToString();
        var role = AdminRoleName;

        testAuthor.Followers.Add(testUser);

        _authorRepositoryMock.Setup(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId))).ReturnsAsync(testAuthor);
        _userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role))).ReturnsAsync(false);

        var expected = new AuthorDetailsViewModel()
        {
            Id = authorId,
            Alias = testAuthor.Alias,
            Name = testAuthor.Name,
            Description = testAuthor.Description,
            IsActive = testAuthor.IsActive,
            FollowerCount = 1,
            IsUserFollowing = true,
            SubscriberCount = 1,
            IsUserSubscribed = false,
        };

        // Act
        var result = await _authorService.GetAuthorDetailsAsync(authorId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAuthorDetailsByIdAsync(It.Is<string>(x => x == authorId)), Times.Once);
        _userManagerMock.Verify(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.Is<string>(x => x == role)), Times.Never);
    }
}
