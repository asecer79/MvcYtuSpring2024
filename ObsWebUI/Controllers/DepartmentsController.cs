using DataAccess.Dal.Abstract;
using Entities.ObsEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ObsWebUI.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentDal _departmentDal;
        private readonly IFacultyDal _faultyDal;

        public DepartmentsController(IDepartmentDal departmentDal, IFacultyDal faultyDal)
        {
            _departmentDal = departmentDal;
            _faultyDal = faultyDal;
        }

        // GET: Departments
        public IActionResult Index()
        {
            return View( _departmentDal.GetList());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentDal.Get(p => p.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewBag.faculties = _faultyDal.GetList().OrderBy(p => p.Name).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FacultyId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentDal.Add(department);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.faculties = _faultyDal.GetList().OrderBy(p => p.Name).ToList();

            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentDal.Get(p => p.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            ViewBag.faculties = _faultyDal.GetList().OrderBy(p => p.Name).ToList();

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FacultyId,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentDal.Update(department);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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

            ViewBag.faculties = _faultyDal.GetList().OrderBy(p => p.Name).ToList();
            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _departmentDal.Get(p => p.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _departmentDal.Get(p => p.Id == id);
            if (department != null)
            {
                _departmentDal.Remove(department);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _departmentDal.Any(e => e.Id == id);
        }
    }
}
