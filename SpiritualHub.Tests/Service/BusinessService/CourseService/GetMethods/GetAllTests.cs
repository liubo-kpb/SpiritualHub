namespace SpiritualHub.Tests.Service.BusinessService.CourseService.GetMethods;

using Moq;
using MockQueryable.Moq;

using Services.Mappings;
using Client.Infrastructure.Enums;
using Client.ViewModels.Course;
using Data.Configuration.Seed;
using Data.Models;

public class GetAllTests : MockConfiguration
{
    [Test]
    [TestCase(1, 3, "", "", CourseSorting.Newest)] // Regular search
    [TestCase(1, 20, "", "")] // When we wish to load more than the DBs entity count.
    [TestCase(2, 20, "", "")] // If we want to go to the second page of authors that exceed the DBs entities.
    [TestCase(1, 10, "", "", CourseSorting.Oldest)]
    [TestCase(1, 10, "", "", CourseSorting.TopRated)]
    [TestCase(1, 10, "", "", CourseSorting.LeastRated)]
    [TestCase(1, 10, "", "", CourseSorting.PriceAscending)]
    [TestCase(1, 10, "", "", CourseSorting.PriceDescending)]
    [TestCase(1, 10, "", "O")]
    [TestCase(1, 10, "Experience", "")]
    [TestCase(1, 10, "Aca", "sci")]
    [TestCase(1, 10, "!@#$%", "!@#$%")]
    public async Task MultipleCases(int page, int entitiesPerPage, string categoryName, string searchTerm, CourseSorting sortingOption = CourseSorting.Newest)
    {
        // Arrange
        var queryModel = new AllCoursesQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _courses.Count - 1,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        _courses[2].IsActive = false;

        GetCourseWithStudent();
        AssignCourseForeignEntities();


        var expectedList = new List<CourseViewModel>();
        _mapper.MapListToViewModel(FilterExpectedCourses(queryModel), expectedList);

        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        // Act
        var result = await _courseService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Courses, Is.EqualTo(expectedList));
            Assert.That(result.Courses.All(b => b.IsActive), Is.True);
            Assert.That(result.TotalCoursesCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    [Test]
    [TestCase(2, 3, "", "")]
    public async Task WhenOnSecondPage_DifferentEntittiesVerification(int page, int entitiesPerPage, string categoryName, string searchTerm, CourseSorting sortingOption = CourseSorting.Newest)
    {
        // Arrange
        var queryModel = new AllCoursesQueryModel()
        {
            CategoryName = categoryName,
            SearchTerm = searchTerm,
            CurrentPage = page,
            EntitiesPerPage = entitiesPerPage,
            TotalEntitiesCount = _courses.Count,
            SortingOption = sortingOption,
        };

        var userId = _users.First().Id.ToString();

        GetCourseWithStudent();
        AssignCourseForeignEntities();

        var expectedList = new List<CourseViewModel>();
        _mapper.MapListToViewModel(FilterExpectedCourses(queryModel), expectedList);

        var wrongQuery = new AllCoursesQueryModel()
        {
            CategoryName = string.Empty,
            SearchTerm = string.Empty,
            CurrentPage = 1,
            EntitiesPerPage = 6,
            TotalEntitiesCount = _courses.Count,
            SortingOption = CourseSorting.Oldest,
        };

        var notExpectedList = new List<CourseViewModel>();
        _mapper.MapListToViewModel(FilterExpectedCourses(wrongQuery), notExpectedList);


        _courseRepositoryMock.Setup(x => x.AllAsNoTracking()).Returns(_courses.AsQueryable().BuildMock());

        // Act
        var result = await _courseService.GetAllAsync(queryModel, userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Courses, Is.EqualTo(expectedList));
            Assert.That(result.Courses, Is.Not.EqualTo(notExpectedList));
            Assert.That(result.TotalCoursesCount, Is.EqualTo(queryModel.TotalEntitiesCount));
        });
        _courseRepositoryMock.Verify(x => x.AllAsNoTracking(), Times.Once);
    }

    private IEnumerable<Course> FilterExpectedCourses(AllCoursesQueryModel queryModel)
    {
        var filteredCourses = _courses.Where(c => c.IsActive);

        if (!string.IsNullOrWhiteSpace(queryModel.CategoryName))
        {
            filteredCourses = filteredCourses.Where(c => c.Category != null && c.Category!.Name == queryModel.CategoryName);
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
        {
            string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
            filteredCourses = filteredCourses.Where(c => c.Name.ToLower().Contains(wildCard)
                                                || c.Author.Name.ToLower().Contains(wildCard)
                                                || c.Author.Alias.ToLower().Contains(wildCard)
                                                || c.Description.ToLower().Contains(wildCard)
                                                || c.ShortDescription.ToLower().Contains(wildCard));
        }

        filteredCourses = queryModel.SortingOption switch
        {
            CourseSorting.Newest => filteredCourses.OrderByDescending(c => c.AddedOn),
            CourseSorting.Oldest => filteredCourses.OrderBy(c => c.AddedOn),
            CourseSorting.StudentsDescending => filteredCourses.OrderByDescending(c => c.Students.Count),
            CourseSorting.StudentsAscending => filteredCourses.OrderBy(c => c.Students.Count),
            CourseSorting.PriceDescending => filteredCourses.OrderByDescending(c => c.Price),
            CourseSorting.PriceAscending => filteredCourses.OrderBy(c => c.Price),
            CourseSorting.TopRated => filteredCourses.OrderByDescending(c => c.Ratings.Count == 0 ? 0 : (c.Ratings.Sum(r => r.Stars) / (c.Ratings.Count * 1.0))),
            CourseSorting.LeastRated => filteredCourses.OrderBy(c => c.Ratings.Count == 0 ? 0 : (c.Ratings.Sum(r => r.Stars) / (c.Ratings.Count * 1.0))),
            _ => filteredCourses.OrderByDescending(c => c.AddedOn),
        };

        return filteredCourses
            .Skip((queryModel.CurrentPage - 1) * queryModel.EntitiesPerPage)
            .Take(queryModel.EntitiesPerPage);
    }

    private void AssignCourseForeignEntities()
    {
        var categoryList = new SeedCategoryConfiguration().GenerateEntities();
        foreach (var course in _courses)
        {
            course.Category = categoryList.FirstOrDefault(c => c.Id == course.CategoryID);
            course.Author = _authors.FirstOrDefault(a => a.Id == course.AuthorID)!;
        }
    }
}
