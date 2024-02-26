namespace SpiritualHub.Client.Filters;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Services.Validation.Interfaces;
using SpiritualHub.Client.Controllers;

public class CustomValidationFilterAttribute : ActionFilterAttribute
{
    private readonly IValidationService _validationService;
    private readonly IAuthorValidationService _authorValidationService;

    public CustomValidationFilterAttribute(
        IValidationService validationService,
        IAuthorValidationService authorValidationService)
    {
        _validationService = validationService;
        _authorValidationService = authorValidationService;
    }

    public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is AuthorController authorController)
        {
            _authorValidationService.User = context.HttpContext.User;
            _authorValidationService.TempData = authorController.TempData;
        }
        else
        {
            _validationService.User = context.HttpContext.User;
            if (context.Controller is Controller controller)
            {
                _validationService.TempData = controller.TempData;
            }
        }

        return base.OnActionExecutionAsync(context, next);
    }
}
