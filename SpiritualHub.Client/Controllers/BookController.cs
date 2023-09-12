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

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllBooksQueryModel queryModel)
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        return View();
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
