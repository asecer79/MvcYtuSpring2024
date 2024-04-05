using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
