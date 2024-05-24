//using System.Diagnostics;
//using Business.Services.Obs.Abstract;
//using Entities.ObsEntities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace ObsWebUI.Controllers
//{
//    [Authorize(Roles = "admin,user")]
//    public class DepartmentsController : Controller
//    {
//        private readonly IDepartmentService _departmentService;
//        private readonly IFacultyService _faultyService;

//        public DepartmentsController(IDepartmentService departmentService, IFacultyService faultyService)
//        {
//            _departmentService = departmentService;
//            _faultyService = faultyService;
//        }

//        // GET: Departments
//        public IActionResult Index()
//        {
           
//            return View( _departmentService.GetList());
//        }

//        // GET: Departments/Details/5
//        public IActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var department = _departmentService.Get(p => p.Id == id);
//            if (department == null)
//            {
//                return NotFound();
//            }

//            return View(department);
//        }

//        [Authorize(Roles = "admin")]
//        public IActionResult Create()
//        {
//            ViewBag.faculties = _faultyService.GetList().OrderBy(p => p.Name).ToList();

//            return View();
//        }

//        [Authorize(Roles = "admin")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create([Bind("Id,FacultyId,Name")] Department department)
//        {
//            if (ModelState.IsValid)
//            {
//                _departmentService.Add(department);
//                return RedirectToAction(nameof(Index));
//            }
//            ViewBag.faculties = _faultyService.GetList().OrderBy(p => p.Name).ToList();

//            return View(department);
//        }

//        // GET: Departments/Edit/5
//        [Authorize(Roles = "admin")]
//        public IActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var department = _departmentService.Get(p => p.Id == id);
//            if (department == null)
//            {
//                return NotFound();
//            }

//            ViewBag.faculties = _faultyService.GetList().OrderBy(p => p.Name).ToList();

//            return View(department);
//        }

//        // POST: Departments/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

//        [Authorize(Roles = "admin")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, [Bind("Id,FacultyId,Name")] Department department)
//        {
//            if (id != department.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _departmentService.Update(department);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!DepartmentExists(department.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.faculties = _faultyService.GetList().OrderBy(p => p.Name).ToList();
//            return View(department);
//        }

//        // GET: Departments/Delete/5
//        [Authorize(Roles = "admin")]
//        public IActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var department = _departmentService.Get(p => p.Id == id);
//            if (department == null)
//            {
//                return NotFound();
//            }

//            return View(department);
//        }

//        // POST: Departments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = "admin")]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var department = _departmentService.Get(p => p.Id == id);
//            if (department != null)
//            {
//                _departmentService.Remove(department);
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private bool DepartmentExists(int id)
//        {
//            return _departmentService.Any(e => e.Id == id);
//        }
//    }
//}
