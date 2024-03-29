using Microsoft.AspNetCore.Mvc;

namespace ObsWebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
