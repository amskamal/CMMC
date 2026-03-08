using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMMS.Data;
using CMMS.Models;

namespace CMMS.Controllers
{
    public class EngineersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Engineers
        public async Task<IActionResult> Index(string searchText, int departmentId, string sortType, int pageSize, int pageNumber)
        {
            ViewBag.AllDepartments = _context.Departments;  //search by another table 
            ViewBag.selectDepartmentId = departmentId;

            ViewBag.currentSearch = searchText; //search by doctors

            List<Engineer> engineers = new List<Engineer>();

            //paging
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.Engineers.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Engineers.OrderBy(e => e.EngFullName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Engineers.OrderByDescending(e => e.EngFullName).ToList());
            }

            // searching

            if (string.IsNullOrWhiteSpace(searchText) == true && departmentId <= 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.Engineers);
            }

            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == true)
            {
                engineers = _context.Engineers.Where(e => e.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                engineers = _context.Engineers.Where(e => e.EngFullName.Contains(searchText.Trim())).ToList();
            }


            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                engineers = _context.Engineers.Where((e => e.DepartmentId == departmentId && e.EngFullName.Contains(searchText.Trim()))).ToList();
            }


            return View(engineers);
        }

        // GET: Engineers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineers
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EngId == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }
        [HttpGet]
        // GET: Engineers/Create
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View();
        }

        // POST: Engineers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Engineer engineer)
        {
            if (ModelState.IsValid == true && engineer.DepartmentId > 0)
            {
                _context.Engineers.Add(engineer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View(engineer);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Engineer engineer = _context.Engineers.Include(e => e.Department).FirstOrDefault(e => e.EngId == id);
            if (engineer == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View(engineer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Engineer engineer)
        {
            if (id != engineer.EngId)
            {
                return NotFound();
            }
            if (ModelState.IsValid == true)
            {
                _context.Engineers.Update(engineer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View(engineer);
        }

        // GET: Engineers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var engineer = await _context.Engineers
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.EngId == id);
            if (engineer == null)
            {
                return NotFound();
            }

            return View(engineer);
        }

        // POST: Engineers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var engineer = await _context.Engineers.FindAsync(id);
            _context.Engineers.Remove(engineer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngineerExists(int id)
        {
            return _context.Engineers.Any(e => e.EngId == id);
        }
    }
}
