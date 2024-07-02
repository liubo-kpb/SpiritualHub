namespace SpiritualHub.Tests.Controller.BaseController;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

using Services.Interfaces;
using Services.Validation.Interfaces;
using System.Security.Claims;

public class MockConfiguration
{
    protected Mock<IAuthorService> _authorServiceMock;
    protected Mock<IPublisherService> _publisherServiceMock;
    protected Mock<ICategoryService> _categoryServiceMock;
    protected Mock<IValidationService> _validationServiceMock;

    internal TestBaseController Controller { get; set; } = null!;

    protected string EntityName { get; set; } = "*Entity*";

    [SetUp]
    public virtual void Setup()
    {
        _authorServiceMock = new Mock<IAuthorService>();
        _publisherServiceMock = new Mock<IPublisherService>();
        _categoryServiceMock = new Mock<ICategoryService>();
        _validationServiceMock = new Mock<IValidationService>();

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(x => x.GetService(typeof(IAuthorService))).Returns(_authorServiceMock.Object);
        serviceProviderMock.Setup(x => x.GetService(typeof(IPublisherService))).Returns(_publisherServiceMock.Object);
        serviceProviderMock.Setup(x => x.GetService(typeof(ICategoryService))).Returns(_categoryServiceMock.Object);

        var urlHelperFactoryMock = new Mock<IUrlHelperFactory>();
        var actionContextAccessorMock = new Mock<IActionContextAccessor>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
             new Claim(ClaimTypes.Name, "TestUser")
        }, "mock"));

        var httpContext = new Mock<HttpContext>();
        httpContext.Setup(c => c.User).Returns(user);

        var controllerContext = new ControllerContext()
        {
            HttpContext = httpContext.Object
        };

        Controller = new TestBaseController(serviceProviderMock.Object, urlHelperFactoryMock.Object, actionContextAccessorMock.Object, _validationServiceMock.Object, EntityName)
        {
            ControllerContext = controllerContext,
            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>()),
        };
    }
}
