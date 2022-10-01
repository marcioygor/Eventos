using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeEventos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IActionResult Index()
    {
        return View();
    }

}