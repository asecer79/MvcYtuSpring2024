using Microsoft.AspNetCore.Mvc;

namespace ObsWebUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
