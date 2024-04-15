namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.GetMethods;

using Moq;

using Client.ViewModels.Author;

public class AllAuthorsByPublisherIdTests : MockConfiguration
{
    [Test]
    public async Task WhenSuccess_IsConnectedToAuthor()
    {
        // Arrange
        string publisherId = _publishers[0].Id.ToString();
        string userId = _publishers[0].UserID.ToString();

        var expected = new List<AuthorViewModel>()
        {
            new AuthorViewModel
            {
                Id = _authors[0].Id.ToString(),
                Alias = _authors[0].Alias,
                Name = _authors[0].Name,
                Description = _authors[0].Description,
                IsActive = _authors[0].IsActive,
                IsUserFollowing = true,
                FollowerCount = 1,
            },
            new AuthorViewModel
            {
                Id = _authors[2].Id.ToString(),
                Alias = _authors[2].Alias,
                Name = _authors[2].Name,
                Description = _authors[2].Description,
                IsActive = _authors[2].IsActive,
            },
        };


        _authorRepositoryMock
            .Setup(x => x.GetAllByPublisherIdAsync(It.Is<string>(y => y == publisherId)))
            .Returns(Task.FromResult(_authors.Where(a => a.Publishers.Any(p => p.Id.ToString() == publisherId)).ToList())!);

        // Act
        var result = await _authorService.AllAuthorsByPublisherIdAsync(publisherId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllByPublisherIdAsync(It.Is<string>(y => y == publisherId)));
    }

    [Test]
    public async Task WhenSuccess_NoConnectedAuthor()
    {
        // Arrange
        string publisherId = _publishers[1].Id.ToString();
        string userId = _publishers[1].UserID.ToString();

        var expected = new List<AuthorViewModel>();

        _authorRepositoryMock
            .Setup(x => x.GetAllByPublisherIdAsync(It.Is<string>(y => y == publisherId)))
            .Returns(Task.FromResult(_authors.Where(a => a.Publishers.Any(p => p.Id.ToString() == publisherId)).ToList())!);

        // Act
        var result = await _authorService.AllAuthorsByPublisherIdAsync(publisherId, userId);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
        _authorRepositoryMock.Verify(x => x.GetAllByPublisherIdAsync(It.Is<string>(y => y == publisherId)));
    }

    public override void Setup()
    {
        base.Setup();
        AddAuthorConnections();
    }

    private void AddAuthorConnections()
    {
        _authors[0].Followers.Add(_users.First(u => u.Id == _publishers[0].UserID));
        ConnectPublishers();
    }

    private void ConnectPublishers()
    {
        _authors[0].Publishers.Add(_publishers[0]);
        _authors[2].Publishers.Add(_publishers[0]);
    }
}
