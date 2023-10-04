namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Data.Models;
using Services.Interfaces;
using Infrastructure.Extensions;
using ViewModels.BaseModels;
using ViewModels.Publisher;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
public abstract class BaseController<TViewModel, TDetailsModel, TFormModel, TQueryModel, TSortingEnum> : Controller
    where TViewModel : class
    where TDetailsModel : class
    where TFormModel : BaseFormModel, new()
    where TQueryModel : BaseQueryModel<TViewModel, TSortingEnum>
    where TSortingEnum : Enum
{
    protected readonly string _entityName;

    protected readonly IAuthorService _authorService;
    protected readonly ICategoryService _categoryService;
    protected readonly IPublisherService _publisherService;

    public BaseController(
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService,
        string entityName)
    {
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
        _entityName = entityName;
    }

    protected abstract Task<bool> ExistsAsync(string id);

    protected abstract Task<TQueryModel> GetAllAsync(TQueryModel queryModel, string userId);

    protected abstract Task<TDetailsModel> GetEntityDetails(string id, string userId);

    protected abstract Task<IEnumerable<TViewModel>> GetAllEntitiesByUserId(string userId);

    protected abstract Task<IEnumerable<TViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId);

    protected abstract Task<string> CreateAsync(TFormModel newEntity);

    protected abstract Task<TFormModel> GetEntityInfoAsync(string id);

    protected abstract Task EditAsync(TFormModel updatedEntityFrom);

    [AllowAnonymous]
    [HttpGet]
    public virtual async Task<IActionResult> All([FromQuery] TQueryModel queryModel)
    {
        try
        {
            queryModel = await GetAllAsync(queryModel, this.User.GetId()!);
            
            var categories = await _categoryService.GetAllAsync();
            queryModel.Categories = categories.Select(c => c.Name);

            return View(queryModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}s");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public virtual async Task<IActionResult> Details(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);
            return RedirectToAction(nameof(All));
        }

        try
        {
            var viewModel = await GetEntityDetails(id, this.User.GetId()!);

            return View(viewModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"loading {_entityName}");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Mine()
    {
        try
        {
            var viewModel = await GetAllEntitiesByUserId(this.User.GetId()!);

            return View(viewModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {_entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> MyPublishings()
    {
        string userId = this.User.GetId()!;
        bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;
            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            var publisherId = await _publisherService.GetPublisherIdAsync(userId);
            var viewModel = await GetEntitiesByPublisherIdAsync(publisherId, userId);

            return View(viewModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {_entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        bool isPublisher = isUserAdmin || await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        var formModel = new TFormModel();

        await GetFormDetailsAsync(formModel, userId, isUserAdmin);
        if (!formModel.Authors.Any())
        {
            TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

            return RedirectToAction(nameof(AuthorController.All), nameof(Author));
        }

        return View(formModel);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add(TFormModel newEntityForm)
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();

        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = await _publisherService.IsConnectedToEntityByUserId<Author>(userId, newEntityForm.AuthorId);
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(newEntityForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(newEntityForm, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(newEntityForm, userId, isUserAdmin);

            return View(newEntityForm);
        }

        try
        {
            newEntityForm.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            string id = await CreateAsync(newEntityForm);
            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create {_entityName}");

            await GetFormDetailsAsync(newEntityForm, userId, isUserAdmin);

            return View(newEntityForm);
        }
    }

    [HttpGet]
    public virtual async Task<IActionResult> Edit(string id)
    {
        try
        {
            var entityFormModel = await GetEntityInfoAsync(id);
            if (entityFormModel == null)
            {
                TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

                return RedirectToAction(nameof(All));
            }

            string userId = this.User.GetId()!;
            bool isUserAdmin = this.User.IsAdmin();

            if (!isUserAdmin)
            {
                bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
                if (!isPublisher)
                {
                    TempData[ErrorMessage] = NotAPublisherErrorMessage;

                    return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
                }

                bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, entityFormModel.AuthorId));
                if (!isConnectedPublisher)
                {
                    TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author");

                    return RedirectToAction(nameof(MyPublishings));
                }
            }

            await GetFormDetailsAsync(entityFormModel, userId, isUserAdmin);

            return View(entityFormModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public virtual async Task<IActionResult> Edit(TFormModel updatedEntityFrom)
    {
        bool exists = await ExistsAsync(updatedEntityFrom.Id!);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        if (!isUserAdmin)
        {
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, updatedEntityFrom.AuthorId));
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(updatedEntityFrom.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(updatedEntityFrom, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetFormDetailsAsync(updatedEntityFrom, userId, isUserAdmin);

            return View(updatedEntityFrom);
        }

        try
        {
            updatedEntityFrom.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            await EditAsync(updatedEntityFrom);
            TempData[SuccessMessage] = string.Format(EditSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(Details), new { id = updatedEntityFrom.Id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"edit {_entityName}");

            await GetFormDetailsAsync(updatedEntityFrom, userId, isUserAdmin);

            return View(updatedEntityFrom);
        }
    }

    protected virtual async Task GetFormDetailsAsync(TFormModel formModel, string userId, bool isUserAdmin = false)
    {
        if (isUserAdmin)
        {
            formModel.Authors = await _authorService.GetAllAsync();

            if (formModel.AuthorId == null)
            {
                formModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                formModel.Publishers = await _authorService.GetConnectedEntities<Publisher, PublisherInfoViewModel>(formModel.AuthorId);
            }
        }
        else
        {
            formModel.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
        }
        formModel.Categories = await _categoryService.GetAllAsync();
    }

    protected virtual async Task ValidateModelAsync(TFormModel formModel, bool isUserAdmin)
    {
        bool isExistingAuthor = await _authorService.Exists(formModel.AuthorId);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(formModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "author"));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(formModel.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(formModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }

        if (isUserAdmin)
        {
            if (!(await _publisherService.ExistsByIdAsync(formModel.PublisherId!)))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
            }
            else if (!(await _publisherService.IsConnectedToEntityByPublisherId<Author>(formModel.PublisherId!, formModel.AuthorId)))
            {
                ModelState.AddModelError(nameof(formModel.PublisherId), WrongPublisherErrorMessage);
            }
        }
    }
}
