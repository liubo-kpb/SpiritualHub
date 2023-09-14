namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Data.Models;
using Services.Interfaces;
using ViewModels.Book;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

[Authorize]
public class BookController : Controller
{
    private readonly string entityname = nameof(Book).ToLower();

    private readonly IBookService _bookService;
    private readonly ICategoryService _categoryService;

    public BookController(
        IBookService bookService,
        ICategoryService categoryService)
    {
        _bookService = bookService;
        _categoryService = categoryService;
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
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> MyPublishings()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(BookFormModel newBook)
    {
        return View();
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
}
