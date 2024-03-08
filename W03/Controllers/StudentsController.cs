using Microsoft.AspNetCore.Mvc;
using W03.Models;

namespace W03.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController()
        {
            SchoolDb.InitializeDb(50);
        }

        public IActionResult Index()
        {
            var students = SchoolDb.Students;
            return View(students);
        }
    }
}
