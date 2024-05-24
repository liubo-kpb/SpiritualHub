namespace SpiritualHub.Tests.Service.ValidationService.AuthorValidation;

using Moq;

using TestClasses;
using Services.Interfaces;

public class MockConfiguration
{
    protected Mock<IAuthorService> _authorServiceMock;
    protected Mock<IPublisherService> _publisherServiceMock;

    protected TestAuthorValidationService _validationService;

    protected const string _url = "*address*/{0}/{1}";

    [SetUp]
    public virtual void Setup()
    {
        _authorServiceMock = new Mock<IAuthorService>();
        _publisherServiceMock = new Mock<IPublisherService>();
        _validationService = new TestAuthorValidationService(_authorServiceMock.Object, _publisherServiceMock.Object)
        {
            ControllerName = ControllerName,
            EntityName = "*entityName*",
        };
    }

    protected string ControllerName { get; } = "DefaultController";
}