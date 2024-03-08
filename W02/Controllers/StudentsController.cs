using Microsoft.AspNetCore.Mvc;

namespace W02.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
