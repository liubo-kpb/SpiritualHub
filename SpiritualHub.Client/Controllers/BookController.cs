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
using SpiritualHub.Services;

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
        var filteredBooks = await _bookService.GetAllAsync(queryModel);
        var categories = await _categoryService.GetAllAsync();

        queryModel.Books = filteredBooks.Books;
        queryModel.Categories = categories.Select(c => c.Name);
        queryModel.TotalBooksCount = filteredBooks.TotalBooksCount;

        return View(queryModel);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var bookModel = await _bookService.GetBookDetailsAsync(id);

        return View(bookModel);
    }

    [HttpGet]
    public async Task<IActionResult> Mine()
    {
        var booksModel = await _bookService.AllBooksByUserIdAsync(this.User.GetId()!);

        return View(booksModel);
    }

    [HttpGet]
    public async Task<IActionResult> MyPublishings()
    {
        var publisherId = await _publisherService.GetPublisherIdAsync(this.User.GetId()!);
        var booksModel = await _bookService.GetBooksByPublisherIdAsync(publisherId);

        return View(booksModel);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        string userId = this.User.GetId()!;
        bool isUserAdmin = this.User.IsAdmin();
        bool isPublisher = isUserAdmin ? true : await _publisherService.ExistsByUserId(userId);
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
            bool isPublisher = await _publisherService.ExistsByUserId(userId);
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookFormModel updatedBook)
    {
        return View();
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
            if (!(await _publisherService.ExistsById(bookFormModel.PublisherId!)))
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
