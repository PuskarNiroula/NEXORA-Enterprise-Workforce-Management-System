using Microsoft.AspNetCore.Mvc;

namespace NEXORA.Controllers;

public class DepartmentController:Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
}