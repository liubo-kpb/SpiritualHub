namespace SpiritualHub.Client.Controllers;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

using Data.Models;
using Services.Interfaces;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using ViewModels.Book;

using static Common.ErrorMessagesConstants;
using static Common.SuccessMessageConstants;

public class BookController : ProductController<BookViewModel, BookDetailsViewModel, BookFormModel, AllBooksQueryModel, BookSorting>
{
    private readonly IBookService _bookService;

    public BookController(
        IBookService bookService,
        IServiceProvider serviceProvider,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor
    )
        : base(serviceProvider, urlHelperFactory, actionContextAccessor, nameof(Book).ToLower())
    {
        _bookService = bookService;
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

    protected override async Task<IEnumerable<BookViewModel>> GetAllEntitiesByUserIdAsync(string userId)
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

    protected override async Task GetAsync(string id, string userId)
    {
        await _bookService.GetAsync(id, userId);
    }

    protected override async Task RemoveAsync(string id, string userId)
    {
        await _bookService.RemoveAsync(id, userId);
    }

    protected override async Task DeleteAsync(string id)
    {
        await _bookService.DeleteAsync(id);
    }

    protected override async Task ShowAsync(string id)
    {
        await _bookService.ShowAsync(id);
    }

    protected override async Task HideAsync(string id)
    {
        await _bookService.HideAsync(id);
    }

    protected override async Task<string> GetAuthorIdAsync(string entityId)
    {
        return await _bookService.GetAuthorIdAsync(entityId);
    }

    protected override async Task<bool> HasEntityAsync(string id, string usedId)
    {
        return await _bookService.HasBookAsync(id, usedId);
    }

    protected override string AlreadyHasEntityErrorMessage()
    {
        return AlreadyHasBookErrorMessage;
    }

    protected override string GetEntitySuccessMessage()
    {
        return GetBookSuccessMessage;
    }

    protected override string RemoveEntitySuccessMessage()
    {
        return RemoveBookSuccessMessage;
    }

    protected override string GetAction()
    {
        return $"add {_entityName} to your library";
    }

    protected override string RemoveAction()
    {
        return $"remove {_entityName} from your library";
    }

    protected override async Task ValidateModelAsync(BookFormModel formModel)
    {
        if (formModel.Price < 0)
        {
            ModelState.AddModelError(nameof(formModel.Price), PriceMustBeZeroOrHigherErrorMessage);
        }

        await base.ValidateModelAsync(formModel);
    }

    protected override async Task<string> ValidateAccessibilityAsync(string id)
    {
        bool isUserLoggedIn = this.User.Identity?.IsAuthenticated ?? false;

        if (!(await _bookService.IsHiddenAsync(id))
            || (isUserLoggedIn && await UserHasAccess(id)))
        {
            return string.Empty;
        }

        return string.Format(NoEntityFoundErrorMessage, _entityName);
    }

    private async Task<bool> UserHasAccess(string id)
    {
        return await _bookService.HasBookAsync(id, GetUserId()!)
            || await ValidateModifyPermissionsAsync(id);
    }
}
