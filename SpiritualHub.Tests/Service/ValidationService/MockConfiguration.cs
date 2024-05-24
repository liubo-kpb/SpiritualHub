namespace SpiritualHub.Tests.Service.ValidationService;

using Moq;

using TestClasses;
using Services.Interfaces;

public class MockConfiguration
{
    protected Mock<IPublisherService> _publisherServiceMock;

    protected TestValidationService _validationService;

    protected const string _url = "*address*/{0}/{1}";

    [SetUp]
    public virtual void Setup()
    {
        _publisherServiceMock = new Mock<IPublisherService>();
        _validationService = new TestValidationService(_publisherServiceMock.Object)
        {
            ControllerName = ControllerName,
            EntityName = "*entityName*",
        };
    }

    protected string ControllerName { get; } = "DefaultController";
}
