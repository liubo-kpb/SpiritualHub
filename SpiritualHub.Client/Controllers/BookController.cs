namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Data.Models;
using Services.Interfaces;
using ViewModels.Book;
using ViewModels.Publisher;
using Infrastructure.Extensions;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
public class BookController : Controller
{
    private readonly string entityName = nameof(Book).ToLower();

    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ICategoryService _categoryService;
    private readonly IPublisherService _publisherService;

    public BookController(
        IBookService bookService,
        IAuthorService authorService,
        ICategoryService categoryService,
        IPublisherService publisherService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _categoryService = categoryService;
        _publisherService = publisherService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllBooksQueryModel queryModel)
    {
        try
        {
            var filteredBooks = await _bookService.GetAllAsync(queryModel, this.User.GetId()!);
            var categories = await _categoryService.GetAllAsync();

            queryModel.Books = filteredBooks.Books;
            queryModel.Categories = categories.Select(c => c.Name);
            queryModel.TotalBooksCount = filteredBooks.TotalBooksCount;

            return View(queryModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}s");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            var bookModel = await _bookService.GetBookDetailsAsync(id, this.User.GetId()!);

            return View(bookModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"loading {entityName}");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        try
        {
            var booksModel = await _bookService.AllBooksByUserIdAsync(this.User.GetId()!);

            return View(booksModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> MyPublishings()
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
            var booksModel = await _bookService.GetBooksByPublisherIdAsync(publisherId, userId);

            return View(booksModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load your {entityName}s");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        bool isPublisher = isUserAdmin || await _publisherService.ExistsByUserIdAsync(userId);
        if (!isPublisher)
        {
            TempData[ErrorMessage] = NotAPublisherErrorMessage;

            return RedirectToAction(nameof(PublisherController.Become), nameof(Publisher));
        }

        var formModel = new BookFormModel();

        await GetBookFormDetailsAsync(formModel, userId, isUserAdmin);
        if (!formModel.Authors.Any())
        {
            TempData[ErrorMessage] = NoConnectedAuthorsErrorMessage;

            return RedirectToAction(nameof(AuthorController.All), nameof(Author));
        }

        return View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookFormModel newBookForm)
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

            bool isConnectedPublisher = await _publisherService.IsConnectedToEntityByUserId<Author>(userId, newBookForm.AuthorId);
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(newBookForm.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(newBookForm, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetBookFormDetailsAsync(newBookForm, userId, isUserAdmin);

            return View(newBookForm);
        }

        try
        {
            newBookForm.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            string id = await _bookService.CreateAsync(newBookForm);
            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, entityName);

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create {entityName}");

            await GetBookFormDetailsAsync(newBookForm, userId, isUserAdmin);

            return View(newBookForm);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var bookFormModel = await _bookService.GetBookInfoAsync(id);
            if (bookFormModel == null)
            {
                TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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

                bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, bookFormModel.AuthorId));
                if (!isConnectedPublisher)
                {
                    TempData[ErrorMessage] = string.Format(NotAConnectedPublisherErrorMessage, $"author");

                    return RedirectToAction(nameof(MyPublishings));
                }
            }

            await GetBookFormDetailsAsync(bookFormModel, userId, isUserAdmin);

            return View(bookFormModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookFormModel updatedBookFrom)
    {
        bool exists = await _bookService.ExistsAsync(updatedBookFrom.Id!);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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

            bool isConnectedPublisher = (await _publisherService.IsConnectedToEntityByUserId<Author>(userId, updatedBookFrom.AuthorId));
            if (!isConnectedPublisher)
            {
                ModelState.AddModelError(nameof(updatedBookFrom.AuthorId), string.Format(NoEntityFoundErrorMessage, "affiliated author"));
            }
        }

        await ValidateModelAsync(updatedBookFrom, isUserAdmin);
        if (!ModelState.IsValid)
        {
            await GetBookFormDetailsAsync(updatedBookFrom, userId, isUserAdmin);

            return View(updatedBookFrom);
        }

        try
        {
            updatedBookFrom.PublisherId ??= await _publisherService.GetPublisherIdAsync(userId);

            await _bookService.EditAsync(updatedBookFrom);
            TempData[SuccessMessage] = string.Format(EditSuccessfulMessage, entityName);

            return RedirectToAction(nameof(Details), new { id = updatedBookFrom.Id });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"edit {entityName}");

            await GetBookFormDetailsAsync(updatedBookFrom, userId, isUserAdmin);

            return View(updatedBookFrom);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Get(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, "add book to your library");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Remove(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, "remove book from your library");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            var eventModel = await _bookService.GetBookInfoAsync(id);

            return View(eventModel);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(BookDetailsViewModel bookModel)
    {
        bool exists = await _bookService.ExistsAsync(bookModel.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, entityName);

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"delete {entityName}");

            return View(new { id = bookModel.Id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Hide(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            TempData[SuccessMessage] = HideBookSuccessMessage;

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"hide the {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Show(string id)
    {
        bool exists = await _bookService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

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
            TempData[SuccessMessage] = ShowBookSuccessMessage;

            return RedirectToAction(nameof(MyPublishings));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"show the {entityName}");

            return RedirectToAction(nameof(Details), new { id });
        }
    }

    private async Task GetBookFormDetailsAsync(BookFormModel bookFormModel, string userId, bool isUserAdmin = false)
    {
        if (isUserAdmin)
        {
            bookFormModel.Authors = await _authorService.GetAllAsync();

            if (bookFormModel.AuthorId == null)
            {
                bookFormModel.Publishers = await _publisherService.GetAllAsync();
            }
            else
            {
                bookFormModel.Publishers = await _authorService.GetConnectedEntities<Publisher, PublisherInfoViewModel>(bookFormModel.AuthorId);
            }
        }
        else
        {
            bookFormModel.Authors = await _publisherService.GetConnectedAuthorsAsync(userId);
        }
        bookFormModel.Categories = await _categoryService.GetAllAsync();
    }

    private async Task ValidateModelAsync(BookFormModel bookFormModel, bool isUserAdmin)
    {
        bool isExistingAuthor = await _authorService.Exists(bookFormModel.AuthorId);
        if (!isExistingAuthor)
        {
            ModelState.AddModelError(nameof(bookFormModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "author"));
        }

        bool isExistingCategory = await _categoryService.ExistsAsync(bookFormModel.CategoryId);
        if (!isExistingCategory)
        {
            ModelState.AddModelError(nameof(bookFormModel.CategoryId), string.Format(NoEntityFoundErrorMessage, "category"));
        }

        if (isUserAdmin)
        {
            if (!(await _publisherService.ExistsByIdAsync(bookFormModel.PublisherId!)))
            {
                ModelState.AddModelError(nameof(bookFormModel.PublisherId), string.Format(NoEntityFoundErrorMessage, "publisher"));
            }
            else if (!(await _publisherService.IsConnectedToEntityByPublisherId<Author>(bookFormModel.PublisherId!, bookFormModel.AuthorId)))
            {
                ModelState.AddModelError(nameof(bookFormModel.PublisherId), WrongPublisherErrorMessage);
            }
        }
    }
}
