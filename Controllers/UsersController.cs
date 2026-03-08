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
    public class UsersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Users
        public async Task<IActionResult> Index(string searchText, int departmentId, string sortType, int pageSize, int pageNumber)
        {
            ViewBag.AllDepartments = _context.Departments;  //search by another table 
            ViewBag.selectDepartmentId = departmentId;

            ViewBag.currentSearch = searchText; //search by doctors

            List<User> doctors = new List<User>();

            // paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.Users.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Users.OrderBy(u => u.UserFullName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Users.OrderByDescending(u => u.UserFullName).ToList());
            }

            // searching

            if (string.IsNullOrWhiteSpace(searchText) == true && departmentId <= 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.Users);
            }

            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == true)
            {
                doctors = _context.Users.Where(u => u.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                doctors = _context.Users.Where(u => u.UserFullName.Contains(searchText.Trim())).ToList();
            }


            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                doctors = _context.Users.Where((u => u.DepartmentId == departmentId && u.UserFullName.Contains(searchText.Trim()))).ToList();
            }


            return View(doctors);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(User user)
        {
            if (ModelState.IsValid == true && user.DepartmentId > 0)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View(user);
            }
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            User user = _context.Users.Include(u => u.Department).FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View(user);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {
                
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
