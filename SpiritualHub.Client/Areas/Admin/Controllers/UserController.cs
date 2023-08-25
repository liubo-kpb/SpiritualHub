namespace SpiritualHub.Client.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Interfaces;

public class UserController : BaseAdminController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("Users/All")]
    public async Task<IActionResult> All()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }
}
