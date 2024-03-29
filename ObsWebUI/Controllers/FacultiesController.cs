using Business.Services.Obs.Abstract;
using Entities.ObsEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ObsWebUI.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        // GET: Faculties
        public IActionResult Index()
        {
            return View(_facultyService.GetList());
        }

        // GET: Faculties/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty =  _facultyService.Get(p => p.Id == id);

            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _facultyService.Add(faculty);
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty =  _facultyService.Get(p => p.Id == id) ;

            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Edit(int id, Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _facultyService.Update(faculty);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty =  _facultyService.Get(m=> m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var faculty = _facultyService.Get(p => p.Id == id);
            if (faculty != null)
            {
                _facultyService.Remove(faculty);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return _facultyService.Any(e => e.Id == id);
        }
    }
}
