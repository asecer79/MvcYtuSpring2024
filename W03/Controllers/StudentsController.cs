using Microsoft.AspNetCore.Mvc;
using W03.Models;

namespace W03.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController()
        {
            SchoolDb.InitializeDb(15);
        }

        public IActionResult Index()
        {
            var students = SchoolDb.Students.OrderBy(p=>p.Id).ToList();

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            var maxId = SchoolDb.Students.Max(p => p.Id) + 1;
           
            student.Id=maxId;

            if (ModelState.IsValid)
            {
                SchoolDb.Students.Add(student);

                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int studentId)
        {
            var student = SchoolDb.Students.FirstOrDefault(p => p.Id == studentId);

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {

            if (ModelState.IsValid)
            {
                var toBeRemoved = SchoolDb.Students.FirstOrDefault(p => p.Id == student.Id);

                SchoolDb.Students.Remove(toBeRemoved);

                SchoolDb.Students.Add(student);

                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int studentId)
        {
            var student = SchoolDb.Students.FirstOrDefault(p => p.Id == studentId);

            SchoolDb.Students.Remove(student);

            return RedirectToAction("Index");
        }
    }
}
