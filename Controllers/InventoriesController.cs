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
    public class InventoriesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Inventories
        public async Task<IActionResult> Index(int searchText, string sortType, int pageSize, int pageNumber, int departmentId, int vendId, int equipId)
        {
            ViewBag.AllDepartments = _context.Departments;  //search by Department table 
            ViewBag.selectDepartmentId = departmentId;
            ViewBag.AllVendors = _context.Vendors;  //search by Vendor table 
            ViewBag.selectVendorId = vendId;
            ViewBag.AllEquipments = _context.Equipments;  //search by equipment table 
            ViewBag.selectEquipmentId = equipId;
            ViewBag.currentSearch = searchText; //search by Equipments

            List<Inventory> inventories = new List<Inventory>();

            //paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.Inventories.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Inventories.OrderBy(m => m.InventoryId).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Inventories.OrderByDescending(m => m.InventoryId).ToList());
            }

            // searching

            if (departmentId <= 0 && vendId <= 0 && equipId <= 0 && searchText == 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.Inventories);
            }

            if (departmentId > 0 && vendId <= 0 && equipId <= 0 && searchText <= 0)
            {
                inventories = _context.Inventories.Where(i => i.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && vendId > 0 && equipId <= 0 && searchText <= 0)
            {
                inventories = _context.Inventories.Where(i => i.VenId == vendId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && equipId > 0 && searchText <= 0)
            {
                inventories = _context.Inventories.Where(i => i.EquipmentId == equipId).ToList();
            }

            if (departmentId <= 0 && vendId <= 0 && equipId <= 0 && searchText
                > 0)
            {
                inventories = _context.Inventories.Where(i => i.InventoryId == searchText).ToList();
            }

            return View(inventories);
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Department)
                .Include(i => i.Equipment)
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            ViewBag.AllVendors = _context.Vendors.ToList();
            ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
            return View();

        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inventory inventory)
        {
            if (ModelState.IsValid == true && inventory.EquipmentId > 0)
            {
                Equipment equipment = _context.Equipments.Where(e => e.EqId == inventory.EquipmentId).FirstOrDefault();
                Department department = _context.Departments.Where(d => d.DeptId == equipment.DepartmentId).FirstOrDefault();
                Vendor vendor = _context.Vendors.Where(v => v.VendorId == equipment.VenId).FirstOrDefault();

                inventory.DepartmentId = department.DeptId;
                inventory.VenId = vendor.VendorId;

                Inventory inventory1 = _context.Inventories.Where(i => i.EquipmentId == inventory.EquipmentId).FirstOrDefault();

                if (inventory1 == null)                 // l if hena 3amelha 3shan ma3melsh add fil inventory le equipment akter mn mara, we lama l youer ye3ml keda hayefdal                                         fe nafs l page msh 7ayro7 7eta
                {
                    _context.Add(inventory);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.AllDepartments = _context.Departments.ToList();
                    ViewBag.AllVendors = _context.Vendors.ToList();
                    ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 

                    return View(inventory);
                } 
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 

                return View(inventory);
            }
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DeptId", "DedpartmentDescription", inventory.DepartmentId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "EqId", "EqCurrentStatus", inventory.EquipmentId);
            ViewData["VenId"] = new SelectList(_context.Vendors, "VendorId", "VendorAddress", inventory.VenId);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,EquipmentId,VenId,DepartmentId")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DeptId", "DedpartmentDescription", inventory.DepartmentId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "EqId", "EqCurrentStatus", inventory.EquipmentId);
            ViewData["VenId"] = new SelectList(_context.Vendors, "VendorId", "VendorAddress", inventory.VenId);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Department)
                .Include(i => i.Equipment)
                .Include(i => i.Vendor)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.InventoryId == id);
        }
    }
}
