using Microsoft.AspNetCore.Mvc;

namespace ObsWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public IActionResult Index()
        {
            return View();
        }

    }
}
