using Microsoft.AspNetCore.Mvc;

namespace W02.Controllers
{
    public class RegistrationsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About(int id, string name)
        {


            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(int id, string name)
        {


            return View();
        }

    }
}
