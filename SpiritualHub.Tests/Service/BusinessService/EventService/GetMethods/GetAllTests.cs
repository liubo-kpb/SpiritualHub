namespace SpiritualHub.Tests.Service.BusinessService.EventService.GetMethods;

using Moq;
using MockQueryable.Moq;

using Client.Infrastructure.Enums;
using Client.ViewModels.Event;
using Services.Mappings;
using Data.Configuration.Seed;
using Data.Models;

public class GetAllTests : MockConfiguration
{
    [Test]
    [TestCase(1, 3, "", "", EventSorting.Newest)] // Regular search
    [TestCase(1, 20, "", "")] // When we wish to load more than the DBs entity count.
    [TestCase(2, 20, "", "")] // If we want to go to the second page of authors that exceed the DBs entities.
    [TestCase(1, 10, "", "", EventSorting.Oldest)]
    [TestCase(1, 10, "", "", EventSorting.TopRated)]
    [TestCase(1, 10, "", "", EventSorting.LeastRated)]
    [TestCase(1, 10, "", "", EventSorting.PriceAscending)]
    [TestCase(1, 10, "", "", EventSorting.PriceDescending)]
    [TestCase(1, 10, "", "", EventSorting.Soonest)]
    [TestCase(1, 10, "", "", EventSorting.Latest)]
    [TestCase(1, 10, "", "", EventSorting.ParticipantsAscending)]
    [TestCase(1, 10, "", "", EventSorting.ParticipantsDescending)]
    [TestCase(1, 10, "", "O")]
    [TestCase(1, 10, "Evening", "")]
    [TestCase(1, 10, "Channel", "3")]
    [TestCase(1, 10, "!@#$%", "!@#$%")]
    public async Task MultipleCases(int page, int entitiesPerPage, string categoryName, string searchTerm, EventSorting sortingOption = EventSorting.Newest)
    {
        // Arrange
        var queryModel = new AllEventsQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _events.Count,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        GetEventWithParticipant();
        AssignEventForeignEntities();


        var expectedList = new List<EventViewModel>();
        _mapper.MapListToViewModel(FilterExpectedEvents(queryModel), expectedList);

        _eventRepositoryMock.Setup(x => x.GetAll()).Returns(_events.AsQueryable().BuildMock());

        // Act
        var result = await _eventService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Events, Is.EqualTo(expectedList));
            Assert.That(result.TotalEventsCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _eventRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    [Test]
    [TestCase(2, 3, "", "")]
    public async Task WhenOnSecondPage_DifferentEntittiesVerification(int page, int entitiesPerPage, string categoryName, string searchTerm, EventSorting sortingOption = EventSorting.Newest)
    {
        // Arrange
        var queryModel = new AllEventsQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _events.Count,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        GetEventWithParticipant();
        AssignEventForeignEntities();

        var expectedList = new List<EventViewModel>();
        _mapper.MapListToViewModel(FilterExpectedEvents(queryModel), expectedList);

        var wrongQuery = new AllEventsQueryModel()
        {
            CategoryName = string.Empty,
            SearchTerm = string.Empty,
            CurrentPage = 1,
            EntitiesPerPage = 6,
            TotalEntitiesCount = _events.Count,
            SortingOption = EventSorting.Oldest,
        };

        var notExpectedList = new List<EventViewModel>();
        _mapper.MapListToViewModel(FilterExpectedEvents(wrongQuery), notExpectedList);


        _eventRepositoryMock.Setup(x => x.GetAll()).Returns(_events.AsQueryable().BuildMock());

        // Act
        var result = await _eventService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Events, Is.EqualTo(expectedList));
            Assert.That(result.Events, Is.Not.EqualTo(notExpectedList));
            Assert.That(result.TotalEventsCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _eventRepositoryMock.Verify(x => x.GetAll(), Times.Once);
    }

    private IEnumerable<Event> FilterExpectedEvents(AllEventsQueryModel queryModel)
    {
        IEnumerable<Event> filteredEvents = _events.Where(e => e.StartDateTime > DateTime.Now);
        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            filteredEvents = filteredEvents.Where(e => e.Category != null && e.Category.Name.ToLower().Contains(queryModel.CategoryName.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = queryModel.SearchTerm.ToLower();
            filteredEvents = filteredEvents.Where(e => e.Title.ToLower().Contains(wildCard)
                                                    || e.Description.ToLower().Contains(wildCard)
                                                    || (e.LocationName != null && e.LocationName.ToLower().Contains(wildCard))
                                                    || e.Author.Name.ToLower().Contains(wildCard));

        }

        filteredEvents = queryModel.SortingOption switch
        {
            EventSorting.Newest => filteredEvents.OrderByDescending(e => e.CreatedOn),
            EventSorting.Oldest => filteredEvents.OrderBy(e => e.CreatedOn),
            EventSorting.ParticipantsAscending => filteredEvents.OrderBy(e => e.Participants.Count),
            EventSorting.ParticipantsDescending => filteredEvents.OrderByDescending(e => e.Participants.Count),
            EventSorting.PriceAscending => filteredEvents.OrderBy(e => e.Price),
            EventSorting.PriceDescending => filteredEvents.OrderByDescending(e => e.Price),
            EventSorting.Soonest => filteredEvents.OrderBy(e => e.StartDateTime),
            EventSorting.Latest => filteredEvents.OrderBy(e => e.EndDateTime),
            EventSorting.TopRated => filteredEvents.OrderByDescending(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            EventSorting.LeastRated => filteredEvents.OrderBy(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0))),
            _ => filteredEvents
                    .OrderBy(e => e.StartDateTime)
                    .ThenByDescending(e => e.Ratings.Count == 0 ? 0 : (e.Ratings.Sum(r => r.Stars) / (e.Ratings.Count * 1.0)))
                    .ThenBy(e => e.Price)
        };

        return filteredEvents
                    .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
                    .Take(queryModel.EntitiesPerPage);
    }

    private void AssignEventForeignEntities()
    {
        var categoryList = new SeedCategoryConfiguration().GenerateEntities();
        var authorList = new SeedAuthorConfiguration().GenerateEntities();
        foreach (var eventEntity in _events)
        {
            eventEntity.Category = categoryList.FirstOrDefault(c => c.Id == eventEntity.CategoryID);
            eventEntity.Author = authorList.FirstOrDefault(a => a.Id == eventEntity.AuthorID)!;
        }
    }

    public override void Setup()
    {
        GenerateEntities = true;
        base.Setup();
    }
}
