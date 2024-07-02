namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using ViewModels.Author;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Services.Interfaces;
using Services.Validation.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using static Common.NotificationMessagesConstants;

public class AuthorController : BaseController<AuthorViewModel, AuthorDetailsViewModel, AuthorFormModel, AllAuthorsQueryModel, AuthorSorting>
{
    private readonly IAuthorService _authorService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IAuthorValidationService _authorValidationService;

    public AuthorController(
        IAuthorService authorService,
        ISubscriptionService subscriptionService,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor,
        IServiceProvider serviceProvider,
        IAuthorValidationService authorValidationService)
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, authorValidationService, nameof(Author).ToLower())
    {
        _authorService = authorService;
        _subscriptionService = subscriptionService;
        _authorValidationService = authorValidationService;
    }

    [HttpGet]
    public async Task<IActionResult> Activate(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id, id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var author = await _authorService.GetAuthorInfoAsync(id);

            return View(author);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}");

            return RedirectToAction(nameof(MyPublishings));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Activate(AuthorDetailsViewModel author)
    {
        var validationResult = await ValidateModifyActionAsync(author.Id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await _authorService.ActivateAsync(author.Id);

            TempData[SuccessMessage] = AuthorActivatedSuccessfullyMessage;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"activate {_entityName}");
        }

        return RedirectToAction(nameof(Details), new { id = author.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Disable(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id, id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var author = await _authorService.GetAuthorInfoAsync(id);

            return View(author);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}");

            return RedirectToAction(nameof(MyPublishings));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Disable(AuthorDetailsViewModel author)
    {
        var validationResult = await ValidateModifyActionAsync(author.Id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await _authorService.DisableAsync(author.Id);
            TempData[SuccessMessage] = AuthorDeactivatedSuccessfullyMessage;

            return RedirectToAction(nameof(Details), new { id = author.Id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"disable {_entityName}");

            return RedirectToAction(nameof(MyPublishings));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Follow(string id)
    {
        var validationResult = await _authorValidationService.HandleFollowActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await _authorService.FollowAsync(id, this.User.GetId()!);

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "update your followings list");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unfollow(string id)
    {
        if (!await ExistsAsync(id))
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _authorService.UnfollowAsync(id, this.User.GetId()!);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, $"unfollow the {_entityName}");
        }

        return RedirectToAction(nameof(Mine));
    }

    [HttpGet]
    public async Task<IActionResult> Subscribe(string id)
    {
        var validationResult = await _authorValidationService.CheckSubscribeActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var authorModel = await _authorService.GetAuthorSubscribtionsAsync(id);

            return View(authorModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "create your subscription");

            return RedirectToAction(nameof(All));
        }

    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(AuthorSubscribeFormModel authorSubscriptionForm)
    {
        var validationResult = await _authorValidationService.HandleSubscribeActionAsync(_subscriptionService, authorSubscriptionForm.SubscriptionId, authorSubscriptionForm.Id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            await _authorService.SubscribeAsync(authorSubscriptionForm.Id, authorSubscriptionForm.SubscriptionId, this.User.GetId()!);

            return RedirectToAction(nameof(Mine));
        }
        catch (ArgumentException e)
        {
            TempData[ErrorMessage] = e.Message;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, "update your subscription");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        if (!await ExistsAsync(id))
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _authorService.UnsubscribeAsync(id, this.User.GetId()!);
            TempData[SuccessMessage] = AuthorUnsubscriptionSuccessMessage;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = String.Format(GeneralUnexpectedErrorMessage, $"unsubscribe from the {_entityName}");
        }

        return RedirectToAction(nameof(Mine));
    }

    [HttpPost]
    public async Task<IActionResult> BecomeConnectedPublisher(string id)
    {
        var validationResult = await _authorValidationService.CheckConnectActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var publisher = await _publisherService.GetPublisherAsync(this.User.GetId()!);
            await _authorService.AddPublisherAsync(id, publisher!);

            TempData[SuccessMessage] = AuthorConnectedPublisherSuccessMessage;

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"connect with {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveConnectedPublisher(string id)
    {
        var validationResult = await ValidateModifyActionAsync(id);
        if (validationResult != null)
        {
            return validationResult;
        }

        try
        {
            var publisher = await _publisherService.GetPublisherIdAsync(this.User.GetId()!);
            await _authorService.RemovePublisherAsync(id, publisher!);

            TempData[SuccessMessage] = AuthorRemoveAffilicationSuccessMessage;

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"remove your affiliation with the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    public override async Task<IActionResult> Add(AuthorFormModel newEntityForm)
    {
        await ValidateModelAsync(newEntityForm);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }

        bool isPublisher = this.User.IsAdmin() || await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            string id = await CreateAsync(newEntityForm);
            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create {_entityName}");

            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _authorService.ExistsAsync(id);
    }

    protected override async Task<AllAuthorsQueryModel> GetAllAsync(AllAuthorsQueryModel queryModel, string userId)
    {
        var filteredAuthors = await _authorService.GetAllAsync(queryModel, userId);

        queryModel.EntityViewModels = filteredAuthors.Authors;
        queryModel.TotalEntitiesCount = filteredAuthors.TotalAuthorsCount;

        return queryModel;
    }

    protected override async Task<AuthorDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        return await _authorService.GetAuthorDetailsAsync(id, userId);
    }

    protected override async Task<IEnumerable<AuthorViewModel>> GetAllEntitiesByUserIdAsync(string userId)
    {
        return await _authorService.AllAuthorsByUserIdAsync(userId);
    }

    protected override async Task<IEnumerable<AuthorViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        return await _authorService.AllAuthorsByPublisherIdAsync(publisherId, userId);
    }

    protected override async Task<string> CreateAsync(AuthorFormModel newEntity)
    {
        var publisher = await _publisherService.GetPublisherAsync(this.User.GetId()!);

        return await _authorService.CreateAsync(newEntity, publisher!);
    }

    protected override async Task<AuthorFormModel> GetEntityInfoAsync(string id)
    {
        return await _authorService.GetAuthorInfoAsync(id);
    }

    protected override async Task EditAsync(AuthorFormModel updatedEntityFrom)
    {
        await _authorService.EditAsync(updatedEntityFrom);
    }

    protected override async Task GetFormDetailsAsync(AuthorFormModel formModel, bool getAllPublishers = false)
    {
        getAllPublishers = formModel.Id == null;
        await base.GetFormDetailsAsync(formModel, getAllPublishers);
    }

    protected override async Task ValidateModelAsync(AuthorFormModel formModel)
    {
        bool isExistingCategory = await _categoryService.ExistsAsync(formModel.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(formModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }
    }

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        return entityId;
    }
}
