namespace SpiritualHub.Tests.Controller.ProductController;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Moq;

using Services.Interfaces;
using Services.Validation.Interfaces;

internal class MockConfiguration
{
    // TBD: Review Configurations!
    private Mock<IAuthorService> _authorServiceMock;
    private Mock<IPublisherService> _publisherServiceMock;
    private Mock<ICategoryService> _categoryServiceMock;
    private Mock<IValidationService> _validationServiceMock;

    internal TestProductController Controller { get; set; } = null!;

    public string EntityName { get; set; } = "*Entity*";

    [SetUp]
    public void Setup()
    {
        _authorServiceMock = new Mock<IAuthorService>();
        _publisherServiceMock = new Mock<IPublisherService>();
        _categoryServiceMock = new Mock<ICategoryService>();
        _validationServiceMock = new Mock<IValidationService>();
        var urlHelperFactoryMock = new Mock<IUrlHelperFactory>();
        var actionContextAccessorMock = new Mock<IActionContextAccessor>();

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(x => x.GetService(typeof(IAuthorService))).Returns(_authorServiceMock.Object);
        serviceProviderMock.Setup(x => x.GetService(typeof(IPublisherService))).Returns(_publisherServiceMock.Object);
        serviceProviderMock.Setup(x => x.GetService(typeof(ICategoryService))).Returns(_categoryServiceMock.Object);

        Controller = new TestProductController(serviceProviderMock.Object, urlHelperFactoryMock.Object, actionContextAccessorMock.Object, EntityName);
    }
}
