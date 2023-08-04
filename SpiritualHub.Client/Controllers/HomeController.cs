﻿namespace SpiritualHub.Client.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using ViewModels.Home;

public class HomeController : Controller
{

    public async Task<IActionResult> Index()
    {
        return View(new IndexViewModel());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
