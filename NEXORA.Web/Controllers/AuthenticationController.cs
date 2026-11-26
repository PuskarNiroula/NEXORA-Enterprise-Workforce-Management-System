using Microsoft.AspNetCore.Mvc;

namespace NEXORA.Controllers
{
    [Route("Login")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return View("Login");
        }
    }
}