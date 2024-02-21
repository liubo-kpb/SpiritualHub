namespace SpiritualHub.Client.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Data.Models;
using Services.Interfaces;
using ViewModels.Category;

using static Common.NotificationMessagesConstants;
using static Common.ErrorMessagesConstants;
using static Common.ExceptionErrorMessagesConstants;
using static Common.SuccessMessageConstants;


public class CategoryController : BaseAdminController
{
    private readonly string entityName = nameof(Category).ToLower();

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("Category/All")]
    public async Task<IActionResult> All([FromQuery] AllCategoryQueryModel query)
    {
        try
        {
            query.Categories = await _categoryService.GetAllAsync(query.SearchTerm);

            return View(query);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load {entityName}s");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [HttpGet]
    [Route("Category/Add")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Route("Category/Add")]
    public async Task<IActionResult> Add(CategoryServiceModel newCategory)
    {
        try
        {
            await _categoryService.AddAsync(newCategory.Name);

            TempData[SuccessMessage] = string.Format(CreationSuccessfulMessage, entityName);

            return RedirectToAction(nameof(All));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"create the {entityName}");

            return View();
        }
    }

    [HttpGet]
    [Route("Category/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        bool exists = await _categoryService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            var category = await _categoryService.GetSingleAsync(id);

            return View(category!);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"load the {entityName}");

            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    [Route("Category/Edit/{id}")]
    public async Task<IActionResult> Edit(CategoryServiceModel newCategory)
    {
        bool exists = await _categoryService.ExistsAsync(newCategory.Id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _categoryService.EditAsync(newCategory.Id, newCategory.Name);

            TempData[SuccessMessage] = string.Format(EditSuccessfulMessage, entityName);

            return RedirectToAction(nameof(All));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"edit the {entityName}");
            return RedirectToAction(nameof(All));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        bool exists = await _categoryService.ExistsAsync(id);
        if (!exists)
        {
            TempData[ErrorMessage] = string.Format(NoEntityFoundErrorMessage, entityName);

            return RedirectToAction(nameof(All));
        }

        try
        {
            await _categoryService.DeleteAsync(id);

            TempData[SuccessMessage] = string.Format(DeleteSuccessfulMessage, entityName);

            return RedirectToAction(nameof(All));
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = string.Format(GeneralUnexpectedErrorMessage, $"delete the {entityName}");

            return RedirectToAction(nameof(All));
        }
    }
}
