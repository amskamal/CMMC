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
    public class EquipmentsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Equipments
        public async Task<IActionResult> Index(string searchText, string sortType, int pageSize, int pageNumber, int departmentId, int vendId )
        {
            ViewBag.AllDepartments = _context.Departments;  //search by Department table 
            ViewBag.selectDepartmentId = departmentId;
            ViewBag.AllVendors = _context.Vendors;  //search by Vendor table 
            ViewBag.selectVendorId = vendId;



            ViewBag.currentSearch = searchText; //search by Equipments

            List<Equipment> equipments = new List<Equipment>();

            //paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.Equipments.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Equipments.OrderBy(e => e.EqName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Equipments.OrderByDescending(e => e.EqName).ToList());
            }

            // searching

            if (departmentId <= 0 && vendId <= 0 && string.IsNullOrWhiteSpace(searchText) == true )  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.Equipments);
            }

            if (departmentId > 0 && vendId <=0 && string.IsNullOrWhiteSpace(searchText) == true)
            {
                equipments = _context.Equipments.Where(e => e.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                equipments = _context.Equipments.Where(e => e.EqName.Contains(searchText.Trim())).ToList();
            }

            if (departmentId > 0 && vendId > 0 && string.IsNullOrWhiteSpace(searchText) == true)
            {
                equipments = _context.Equipments.Where(e => e.VenId == vendId).ToList();
            }

            return View(equipments);
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.Department)
                .Include(e => e.Vendor)
                .FirstOrDefaultAsync(m => m.EqId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            ViewBag.AllVendors = _context.Vendors.ToList();
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("EqId,EqSerialNo,EqName,EqInstallatonDate,EqQuantity,EqCost,EqWarrantyDate,EqCurrentStatus,VenId,DepartmentId")] Equipment equipment)
        public IActionResult Create(Equipment equipment)
        {
           if (ModelState.IsValid == true && equipment.DepartmentId > 0 && equipment.VenId > 0)
           {
                equipment.EqCurrentStatus = "Stable";
               _context.Equipments.Add(equipment);
               _context.SaveChanges();
               return RedirectToAction("Index");
           }
           else
           {
               ViewBag.AllDepartments = _context.Departments.ToList();
               ViewBag.AllVendors = _context.Vendors.ToList();
               return View(equipment);
           }
        }

        // GET: Equipments/Edit/5
        public IActionResult Edit(int id)
        {
            Equipment equipment = _context.Equipments.Find(id);
            if (equipment == null)
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                return View(equipment);
            }

            //var equipment = await _context.Equipments.FindAsync(id);
            //if (equipment == null)
            //{
            //    return NotFound();
            //}
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DeptId", "DedpartmentDescription", //equipment.DepartmentId);
            //ViewData["VenId"] = new SelectList(_context.Vendors, "VendorId", "VendorAddress", equipment.VenId);
            //return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Equipment equipment)
        {
            if (id != equipment.EqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {
                _context.Equipments.Update(equipment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllDepartments = _context.Departments.ToList();
            ViewBag.AllVendors = _context.Vendors.ToList();
            return View(equipment);
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DeptId", "DepartmentName", equipment.DepartmentId);
            //ViewData["VenId"] = new SelectList(_context.Vendors, "VendorId", "VendorFullName", equipment.VenId);
            //return View(equipment);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .Include(e => e.Department)
                .Include(e => e.Vendor)
                .FirstOrDefaultAsync(m => m.EqId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipments.Any(e => e.EqId == id);
        }
    }
}
