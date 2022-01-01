using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcElectron.Models;

namespace MvcElectron.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new FilesViewModel());
    }

    public IActionResult DeleteFile(string fileName)
    {
        var viewModel = new FilesViewModel();
        viewModel.DeleteFile(fileName);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
