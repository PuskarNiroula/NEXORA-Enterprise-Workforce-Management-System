using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NEXORA.Controllers;

public class HomeController:Controller
{

    public IActionResult Index()
    {
        return View();
    }
    [Authorize]
    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }
    
}