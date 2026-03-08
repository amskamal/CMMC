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
    public class MaintenancesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Maintenances
        public async Task<IActionResult> Index(int searchText, string sortType, int pageSize, int pageNumber, int departmentId, int vendId, int equipId, int EnginId, int usId)
        {
            ViewBag.AllDepartments = _context.Departments;  //search by Department table 
            ViewBag.selectDepartmentId = departmentId;
            ViewBag.AllVendors = _context.Vendors;  //search by Vendor table 
            ViewBag.selectVendorId = vendId;
            ViewBag.AllEquipments = _context.Equipments;  //search by equipment table 
            ViewBag.selectEquipmentId = equipId;
            ViewBag.AllEngineers = _context.Engineers;  //search by engineer table 
            ViewBag.selectEngineerId = EnginId;
            ViewBag.AllUsers = _context.Users;          // search by user table
            ViewBag.SelectUserId = usId;

            ViewBag.currentSearch = searchText; //search by Equipments

            List<Maintenance> maintenances = new List<Maintenance>();

            //paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.Maintenances.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Maintenances.OrderBy(m => m.MaintenaceId).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Maintenances.OrderByDescending(m => m.MaintenaceId).ToList());
            }

            // searching

            if (departmentId <= 0 && vendId <= 0&& EnginId <= 0 && usId <= 0 && equipId <= 0 && searchText == 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.Maintenances);
            }

            if (departmentId > 0 && vendId <= 0 && EnginId <= 0 && usId <= 0 && equipId <= 0 && searchText <= 0)
            {
                maintenances = _context.Maintenances.Where(m => m.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && vendId > 0 && EnginId <= 0 && usId <= 0 && equipId <= 0 && searchText <= 0)
            {
                maintenances = _context.Maintenances.Where(e => e.VenId == vendId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && EnginId > 0 && usId <= 0 && equipId <= 0 && searchText <= 0)
            {
                maintenances = _context.Maintenances.Where(m => m.EngineerId == EnginId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && EnginId <= 0 && usId > 0 && equipId <= 0 && searchText <= 0)
            {
                maintenances = _context.Maintenances.Where(m => m.UId == usId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && EnginId <= 0 && usId <= 0 && equipId > 0 && searchText <= 0)
            {
                maintenances = _context.Maintenances.Where(m => m.EquipmentId == equipId).ToList();
            }
            if (departmentId <= 0 && vendId <= 0 && EnginId <= 0 && usId <= 0 && equipId <= 0 && searchText 
                > 0)
            {
                maintenances = _context.Maintenances.Where(m => m.MaintenaceId == searchText ).ToList();
            }

            return View(maintenances);
        }

        // GET: Maintenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances
                .Include(m => m.Department)
                .Include(m => m.Engineer)
                .Include(m => m.Equipment)
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MaintenaceId == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // GET: Maintenances/Create
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            ViewBag.AllVendors = _context.Vendors.ToList();
            ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
            ViewBag.AllEngineers = _context.Engineers.ToList();  //search by engineer table 
            ViewBag.AllUsers = _context.Users.ToList();          // search by user table
            return View();
        }

        // POST: Maintenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Maintenance maintenance)
        {

            if (ModelState.IsValid == true && maintenance.EngineerId > 0 && maintenance.EquipmentId > 0 && maintenance.UId > 0)
            {
                Equipment equipment = _context.Equipments.Where(e => e.EqId == maintenance.EquipmentId).FirstOrDefault();
                Vendor vendor = _context.Vendors.Where(v => v.VendorId == equipment.VenId).FirstOrDefault();
                Department department = _context.Departments.Where(d => d.DeptId == equipment.DepartmentId).FirstOrDefault();

                equipment.EqCurrentStatus = "Broken";
                maintenance.RectDate = DateTime.MinValue;
                maintenance.UsingAfterRectDate = DateTime.MinValue;
                maintenance.DownTime = 0;
                maintenance.ResponseTime = 0;
                maintenance.VenId = vendor.VendorId;
                maintenance.DepartmentId = department.DeptId;
                Maintenance maintenance1 = _context.Maintenances.Where(m => m.EquipmentId == maintenance.EquipmentId).FirstOrDefault();

                if (maintenance1 == null)
                {
                    _context.Maintenances.Add(maintenance);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.AllDepartments = _context.Departments.ToList();
                    ViewBag.AllVendors = _context.Vendors.ToList();
                    ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                    ViewBag.AllEngineers = _context.Engineers.ToList();  //search by engineer table 
                    ViewBag.AllUsers = _context.Users.ToList();          // search by user table
                    return View();
                }
               
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                ViewBag.AllEngineers = _context.Engineers.ToList();  //search by engineer table 
                ViewBag.AllUsers = _context.Users.ToList();          // search by user table
                return View();
            }
        }

        // GET: Maintenances/Edit/5
        public IActionResult Edit(int? id)
        {
            Maintenance maintenance = _context.Maintenances.Find(id);

            if (maintenance == null)
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                ViewBag.AllEngineers = _context.Engineers.ToList();  //search by engineer table 
                ViewBag.AllUsers = _context.Users.ToList();
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                ViewBag.AllEngineers = _context.Engineers.ToList();  //search by engineer table 
                ViewBag.AllUsers = _context.Users.ToList();
                return View(maintenance);
            }
        }

        // POST: Maintenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Maintenance maintenance)
        {
            maintenance.MaintenaceId = id;

            if (id != maintenance.MaintenaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {
                Equipment equipment = _context.Equipments.Where(e => e.EqId == maintenance.EquipmentId).FirstOrDefault();
                equipment.EqCurrentStatus = "Stable";

                maintenance.DownTime = (maintenance.RectDate-maintenance.BreakDownDate).Days;
                maintenance.ResponseTime = (maintenance.UsingAfterRectDate-maintenance.BreakDownDate).Days;
                _context.Maintenances.Update(maintenance);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintenance);
        }

        // GET: Maintenances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenances
                .Include(m => m.Department)
                .Include(m => m.Engineer)
                .Include(m => m.Equipment)
                .Include(m => m.User)
                .Include(m => m.Vendor)
                .FirstOrDefaultAsync(m => m.MaintenaceId == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenance = await _context.Maintenances.FindAsync(id);
            _context.Maintenances.Remove(maintenance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceExists(int id)
        {
            return _context.Maintenances.Any(e => e.MaintenaceId == id);
        }
    }
}
