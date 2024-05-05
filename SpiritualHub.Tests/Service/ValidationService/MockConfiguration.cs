namespace SpiritualHub.Tests.Service.ValidationService;

using Microsoft.AspNetCore.Mvc;

using Moq;

using TestClasses;
using TestClasses.Interfaces;
using Services.Interfaces;

public class MockConfiguration
{
    protected Mock<IAuthorService> _authorServiceMock;
    protected Mock<IPublisherService> _publisherServiceMock;
    protected Mock<IUrlHelper> _urlHelperMock;

    protected ITestValidationService _validationService;

    protected const string _url = "*address*/{0}/{1}";

    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        _authorServiceMock = new Mock<IAuthorService>();
        _publisherServiceMock = new Mock<IPublisherService>();
    }

    [SetUp]
    public virtual void Setup()
    {
        _validationService = new TestValidationService(_authorServiceMock.Object, _publisherServiceMock.Object)
        {
            ControllerName = ControllerName,
            EntityName = "*entityName*",
        };
    }

    protected string ControllerName { get; set; } = "DefaultController";
}
