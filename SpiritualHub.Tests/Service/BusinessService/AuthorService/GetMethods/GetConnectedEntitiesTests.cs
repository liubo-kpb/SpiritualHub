namespace SpiritualHub.Tests.Service.BusinessService.AuthorService.GetMethods;

using Moq;
using NuGet.Packaging;

using Data.Configuration.Seed;
using Data.Models;
using Services.Mappings;
using Client.ViewModels.Publisher;
using Client.ViewModels.User;
using Client.ViewModels.Subscription;
using Client.ViewModels.Book;
using Client.ViewModels.Event;
using Client.ViewModels.Course;

public class GetConnectedEntitiesTests : MockConfiguration
{
    [Test]
    public async Task GetAllPublishers()
    {
        // Arrange
        var testAuthor = _authors[3];
        testAuthor.Publishers.AddRange(_publishers);

        var expeceted = new List<PublisherInfoViewModel>();
        _mapper.MapListToViewModel(testAuthor.Publishers, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Publishers";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<Publisher>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<Publisher, PublisherInfoViewModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<Publisher>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }

    [Test]
    public async Task GetAllFollowers()
    {
        // Arrange
        var testAuthor = _authors[3];
        testAuthor.Followers.AddRange(_users);

        var expeceted = new List<UserServiceModel>();
        _mapper.MapListToViewModel(testAuthor.Followers, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Followers";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<ApplicationUser>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<ApplicationUser, UserServiceModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<ApplicationUser>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }

    [Test]
    public async Task GetAllSubscriptions()
    {
        // Arrange
        var testAuthor = GetAuthorWithSubscriber();

        var expeceted = new List<SubscriptionViewModel>();
        _mapper.MapListToViewModel(testAuthor.Subscriptions, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Subscriptions";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<Subscription>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<Subscription, SubscriptionViewModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<Subscription>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }

    [Test]
    public async Task GetAllBooks()
    {
        // Arrange
        var testAuthor = _authors[3];
        var books = new SeedBookConfiguration().GenerateEntities().Where(b => b.AuthorID == testAuthor.Id);
        testAuthor.Books.AddRange(books);

        var expeceted = new List<BookInfoViewModel>();
        _mapper.MapListToViewModel(testAuthor.Books, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Books";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<Book>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<Book, BookInfoViewModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<Book>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }

    [Test]
    public async Task GetAllEvents()
    {
        // Arrange
        var testAuthor = _authors[3];
        var events = new SeedEventConfiguration().GenerateEntities().Where(e => e.AuthorID == testAuthor.Id);
        testAuthor.Events.AddRange(events);

        var expeceted = new List<EventInfoViewModel>();
        _mapper.MapListToViewModel(testAuthor.Events, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Events";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<Event>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<Event, EventInfoViewModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<Event>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }

    [Test]
    public async Task GetAllCourses()
    {
        // Arrange
        var testAuthor = _authors[3];
        var courses = new SeedCourseConfiguration().GenerateEntities().Where(c => c.AuthorID == testAuthor.Id);
        testAuthor.Courses.AddRange(courses);

        var expeceted = new List<CourseInfoViewModel>();
        _mapper.MapListToViewModel(testAuthor.Courses, expeceted);

        var authorId = testAuthor.Id.ToString();
        var propertyName = "Courses";

        _authorRepositoryMock.Setup(x => x.GetAuthorWithEntitiesAsync<Course>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName))).ReturnsAsync(testAuthor);

        // Act
        var result = await _authorService.GetConnectedEntitiesAsync<Course, CourseInfoViewModel>(authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null, "Collection returned was null.");
            Assert.That(result, Is.EqualTo(expeceted), "Resulted and expected collections did not match.");
        });
        _authorRepositoryMock.Verify(x => x.GetAuthorWithEntitiesAsync<Course>(It.Is<string>(x => x == authorId), It.Is<string>(x => x == propertyName)));
    }
}
