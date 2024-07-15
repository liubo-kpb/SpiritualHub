namespace SpiritualHub.Tests.Controller.ProductController;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;

using Services.Interfaces;
using Services.Validation.Interfaces;

public class MockConfiguration
{
    protected Mock<IAuthorService> _authorServiceMock;
    protected Mock<IPublisherService> _publisherServiceMock;
    protected Mock<ICategoryService> _categoryServiceMock;
    protected Mock<IValidationService> _validationServiceMock;

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
        serviceProviderMock.Setup(x => x.GetService(typeof(IValidationService))).Returns(_validationServiceMock.Object);

        Controller = new TestProductController(serviceProviderMock.Object, urlHelperFactoryMock.Object, actionContextAccessorMock.Object, EntityName)
        {
            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>()),
        };
    }
}
