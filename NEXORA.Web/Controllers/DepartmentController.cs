using Microsoft.AspNetCore.Mvc;

namespace NEXORA.Controllers;

public class DepartmentController:Controller
{
    public IActionResult Index()
    {
        return View();
        
    }
}