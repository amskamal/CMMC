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
    public class VendorsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Vendors
        public async Task<IActionResult> Index(string searchText, string sortType, int pageSize, int pageNumber, int venId)
        {
            ViewBag.currentSearch = searchText;
            ViewBag.AllVendors = _context.Vendors;
            ViewBag.selectVendorId = venId;
            List<Vendor> vendors = new List<Vendor>();

            //Paging
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNumber = pageNumber;
                return View(_context.Vendors.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.Vendors.OrderBy(ven => ven.VendorFullName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.Vendors.OrderByDescending(ven => ven.VendorFullName).ToList());
            }

            //search

            if (string.IsNullOrWhiteSpace(searchText) == true && venId <= 0)
            {
                vendors = _context.Vendors.ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == false && venId <= 0)
            {
                vendors = _context.Vendors.Where(ven => ven.VendorFullName.Contains(searchText.Trim())).ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == true && venId > 0)
            {
                vendors = _context.Vendors.Where(ven => ven.VendorId == venId).ToList();
            }
            if (string.IsNullOrWhiteSpace(searchText) == false && venId > 0)
            {
                vendors = _context.Vendors.Where(ven => ven.VendorId == venId && ven.VendorFullName.Contains(searchText.Trim())).ToList();
            }

            return View(vendors);

        }

        // GET: Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Vendors.Add(vendor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        
        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorId,VendorFullName,VendorEmail,VendorConfirmEmaill,VendorPhoneNo,VendorAddress")] Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.VendorId))
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
            return View(vendor);
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendor vendor)
        {
            if (id != vendor.VendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {

                _context.Vendors.Update(vendor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.AllDepartments = _context.Departments.ToList();
            return View(vendor);
        }
        */
        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.VendorId == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.VendorId == id);
        }
    }
}
