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
    public class DepartmentsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Departments
        public async Task<IActionResult> Index(string searchText, string sortType, int pageSize, int pageNo, int departmentId)
        {
            ViewBag.currentSearch = searchText;
            ViewBag.AllDepartments = _context.Departments;
            ViewBag.selectDepartmentId = departmentId;
            List<Department> departments = new List<Department>();

            //Paging
            if (pageSize > 0 && pageNo > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNumber = pageNo;
                return View(_context.Departments.Skip(pageSize * (pageNo - 1)).Take(pageSize));
            }

            //sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Departments.OrderBy(dep => dep.DepartmentName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Departments.OrderByDescending(dep => dep.DepartmentName).ToList());
            }

            //search

            if (string.IsNullOrWhiteSpace(searchText) == true && departmentId <= 0)
            {
                departments = _context.Departments.ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == false && departmentId <= 0)
            {
                departments = _context.Departments.Where(dep => dep.DedpartmentDescription.Contains(searchText.Trim())).ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == true && departmentId > 0)
            {
                departments = _context.Departments.Where(d => d.DeptId == departmentId).ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == false && departmentId > 0)
            {
                departments = _context.Departments.Where(d => d.DeptId == departmentId && d.DepartmentName.Contains(searchText.Trim())).ToList();
            }

            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptId,DepartmentName,DedpartmentDescription")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeptId,DepartmentName,DedpartmentDescription")] Department department)
        {
            if (id != department.DeptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DeptId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DeptId == id);
        }
    }
}
