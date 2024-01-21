namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;

using Data.Models;
using Services.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using ViewModels.Book;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;


public class BookController : BaseController<BookViewModel, BookDetailsViewModel, BookFormModel, AllBooksQueryModel, BookSorting>
{
    private readonly IBookService _bookService;

    public BookController(
        IBookService bookService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
        : base(authorService, categoryService, publisherService, nameof(Book).ToLower())
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> Get(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        string userId = this.User.GetId()!;
        bool hasBook = await _bookService.HasBookAsync(id, userId);
        if (hasBook)
        {
            TempData[ErrorMessage] = AlreadyHasBookErrorMessage;

            return RedirectToAction(nameof(Details), new { id });
        }

        try
        {
            await _bookService.GetAsync(id, userId);
            TempData[SuccessMessage] = GetBookSuccessMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"add {_entityName} to your library");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Remove(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _bookService.RemoveAsync(id, this.User.GetId()!);
            TempData[SuccessMessage] = RemoveBookSuccessMessage;

            return RedirectToAction(nameof(Mine));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"remove {_entityName} from your library");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _bookService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author");

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            var bookModel = await GetEntityInfoAsync(id);

            return View(bookModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(BookDetailsViewModel bookModel)
    {
        bool exists = await ExistsAsync(bookModel.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        bool isPublisher = this.User.IsAdmin() ? true : await _publisherService.ExistsByUserIdAsync(this.User.GetId()!);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        try
        {
            await _bookService.DeleteAsync(bookModel.Id);
            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"delete {_entityName}");

            return View(new { id = bookModel.Id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Hide(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _bookService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author");

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _bookService.HideAsync(id);
            TempData[SuccessMessage] = string.Format(HideEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"hide the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Show(string id)
    {
        bool exists = await ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, _entityName);

            return RedirectToAction(nameof(All));
        }

        if (!this.User.IsAdmin())
        {
            string userId = this.User.GetId()!;
            bool isPublisher = await _publisherService.ExistsByUserIdAsync(userId);
            if (!isPublisher)
            {
                TempData[ErrorMessage] = NotAPublisherErrorMessage;

                return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
            }

            string authorId = await _bookService.GetAuthorIdAsync(id);
            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, authorId));
            if (!isConnectedPublisher)
            {
                TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author");

                return RedirectToAction(nameof(MyPublishings));
            }
        }

        try
        {
            await _bookService.ShowAsync(id);
            TempData[SuccessMessage] = string.Format(ShowEntitySuccessMessage, _entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"show the {_entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    protected override async Task<bool> ExistsAsync(string id)
    {
        return await _bookService.ExistsAsync(id);
    }

    protected override async Task<AllBooksQueryModel> GetAllAsync(AllBooksQueryModel queryModel, string userId)
    {
        var filteredBooks = await _bookService.GetAllAsync(queryModel, userId);

        queryModel.EntityViewModels = filteredBooks.Books;
        queryModel.TotalEntitiesCount = filteredBooks.TotalBooksCount;

        return queryModel;
    }

    protected override async Task<BookDetailsViewModel> GetEntityDetails(string id, string userId)
    {
        return await _bookService.GetBookDetailsAsync(id, userId);
    }

    protected override async Task<IEnumerable<BookViewModel>> GetAllEntitiesByUserId(string userId)
    {
        return await _bookService.AllBooksByUserIdAsync(userId);
    }

    protected override async Task<IEnumerable<BookViewModel>> GetEntitiesByPublisherIdAsync(string publisherId, string userId)
    {
        return await _bookService.GetBooksByPublisherIdAsync(publisherId, userId);
    }

    protected override async Task<string> CreateAsync(BookFormModel newEntity)
    {
        return await _bookService.CreateAsync(newEntity);
    }

    protected override async Task<BookFormModel> GetEntityInfoAsync(string id)
    {
        return await _bookService.GetBookInfoAsync(id);
    }

    protected override async Task EditAsync(BookFormModel updatedEntityFrom)
    {
        await _bookService.EditAsync(updatedEntityFrom);
    }

    protected override async Task ValidateModelAsync(BookFormModel formModel, bool isUserAdmin)
    {
        if (formModel.Price < 0)
        {
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeHigherThanZeroErrorMessage);
        }

        await base.ValidateModelAsync(formModel, isUserAdmin);
    }
}
