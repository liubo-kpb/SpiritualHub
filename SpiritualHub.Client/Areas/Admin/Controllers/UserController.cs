namespace SpiritualHub.Client.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Services.Interfaces;
using Client.ViewModels.User;

using static Common.GeneralApplicationConstants;

public class UserController : BaseAdminController
{
    private readonly IUserService _userService;
    private readonly IMemoryCache _memoryCache;

    public UserController(
        IUserService userService,
        IMemoryCache memoryCache)
    {
        _userService = userService;
        _memoryCache = memoryCache;
    }

    [Route("Users/All")]
    public async Task<IActionResult> All()
    {
        var users = _memoryCache.Get<IEnumerable<UserServiceModel>>(UserCacheKey);
        if (users == null)
        {
            users = await _userService.GetAllAsync();

            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan
                    .FromMinutes(UsersCacheDurationMinutes));

            _memoryCache.Set(UserCacheKey, users, cacheOptions);
        }

        return View(users);
    }
}
