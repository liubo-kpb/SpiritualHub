namespace SpiritualHub.Client.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Author;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Services.Interfaces;
using Data.Models;

using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;
using static Common.NotificationMessagesConstants;

public class AuthorController : BaseController<AuthorViewModel, AuthorDetailsViewModel, AuthorFormModel, AllAuthorsQueryModel, AuthorSorting>
{
    private readonly ISubscriptionService _subscriptionService;

    public AuthorController(
        ISubscriptionService subscriptionService,
        IServiceProvider serviceProvider)
        : base(serviceProvider, nameof(Author).ToLower())
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public async Task<IActionResult> Activate(string id)
    {
        var validationResult = await ValidateAction(id);
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
        var validationResult = await ValidateAction(author.Id);
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
        var validationResult = await ValidateAction(id);
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
        var validationResult = await ValidateAction(author.Id);
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
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        bool isFollowing = await _authorService.IsFollowedByUserWithId(id, this.User.GetId()!);
        if (isFollowing)
        {
            TempData[ErrorMessage] = AlreadyFollowingAuthorErrorMessage;

            return RedirectToAction(nameof(Mine));
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
        var exists = await ExistsAsync(id);
        if (!exists)
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
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        bool isPublisher = !this.User.IsAdmin() || await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (isPublisher)
        {
            TempData[ErrorMessage] = PublishersCannotSubscribeErrorMessage;

            return RedirectToAction(nameof(All));
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
        bool exists = await ExistsAsync(authorSubscriptionForm.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        bool isExistingSubscription = await _subscriptionService.ExistsByIdAsync(authorSubscriptionForm.SubscriptionId);
        if (!isExistingSubscription)
        {
            TempData[ErrorMessage] = SelectValidSubscriptionPlan;

            return View(await _authorService.GetAuthorSubscribtionsAsync(authorSubscriptionForm.Id));
        }

        string userId = this.User.GetId()!;
        bool isPublisher = !this.User.IsAdmin() || await _publisherService.ExistsByUserIdAsync(userId);
        if (isPublisher)
        {
            TempData[ErrorMessage] = PublishersCannotSubscribeErrorMessage;

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _authorService.SubscribeAsync(authorSubscriptionForm.Id, authorSubscriptionForm.SubscriptionId, userId);

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
        var exists = await ExistsAsync(id);
        if (!exists)
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
        var exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = String.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        if (!this.User.IsAdmin())
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = await _publisherService.IsConnectedToAuthorByUserId(userId, id);
            if (isConnectedPublisher)
            {
                TempData[ErrorMessage] = AlreadyAConnectedPublisherErrorMessage;

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            var publisher = await _publisherService.GetPublisherAsync(userId);
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
        var validationResult = await ValidateAction(id);
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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"remove your connection with the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    public override async Task<IActionResult> Add()
    {
        bool isPublisher = this.User.IsAdmin() || await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var formModel = CreateFormModelInstance();
            await GetFormDetailsAsync(formModel);

            return View(nameof(Add), formModel);
        }
        catch (NotImplementedException e)
        {
            TempData[ErrorMessage] = e.Message;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, "load page");
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    public override async Task<IActionResult> Add(AuthorFormModel newEntityForm)
    {
        await ValidateModelAsync(newEntityForm);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(newEntityForm);

            return View(newEntityForm);
        }

        if (!this.User.IsAdmin())
        {
            _validationService.AuthorId = newEntityForm.AuthorId!;
            var validationResult = await _validationService.ModifyPermissionsAsync(this.User.GetId()!, TempData);
            if (validationResult != null)
            {
                return validationResult;
            }
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

    protected override async Task<IEnumerable<AuthorViewModel>> GetAllEntitiesByUserId(string userId)
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

        return await _authorService.CreateAuthor(newEntity, publisher!);
    }

    protected override async Task<AuthorFormModel> GetEntityInfoAsync(string id)
    {
        return await _authorService.GetAuthorInfoAsync(id);
    }

    protected override async Task EditAsync(AuthorFormModel updatedEntityFrom)
    {
        await _authorService.EditAsync(updatedEntityFrom);
    }

    protected override async Task GetFormDetailsAsync(AuthorFormModel formModel)
    {
        if (this.User.IsAdmin() && formModel.Id == null)
        {
            formModel.Publishers = await _publisherService.GetAllAsync();
        }

        formModel.Categories = await _categoryService.GetAllAsync();
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
