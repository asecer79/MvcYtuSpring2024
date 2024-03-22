using DataAccess.Dal.Abstract;
using Entities.ObsEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ObsWebUI.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly IFacultyDal _facultyDal;

        public FacultiesController(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        // GET: Faculties
        public IActionResult Index()
        {
            return View(_facultyDal.GetList());
        }

        // GET: Faculties/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty =  _facultyDal.Get(p => p.Id == id);

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
                _facultyDal.Add(faculty);
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

            var faculty =  _facultyDal.Get(p => p.Id == id);

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
                    _facultyDal.Update(faculty);
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

            var faculty =  _facultyDal.Get(m=> m.Id == id);
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
            var faculty = _facultyDal.Get(p => p.Id == id);
            if (faculty != null)
            {
                _facultyDal.Remove(faculty);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return _facultyDal.Any(e => e.Id == id);
        }
    }
}
