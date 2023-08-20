namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc;

using SpiritualHub.Services.Interfaces;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> All()
    {
        var model = await _categoryService.GetAllAsync();

        return View(model);
    }
}
