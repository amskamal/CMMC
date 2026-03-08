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
    public class ContractsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();


        // GET: Contracts
        public  IActionResult Index(int searchText, string sortType, int pageSize, int pageNumber, int vendId, int equipId)
        {
            ViewBag.AllVendors = _context.Vendors;  //search by Vendor table 
            ViewBag.selectVendorId = vendId;
            ViewBag.AllEquipments = _context.Equipments;  //search by equipment table 
            ViewBag.selectEquipmentId = equipId;

            ViewBag.currentSearch = searchText; //search by Equipments

            List<Contract> contracts = new List<Contract>();

            //paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(_context.contracts.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.contracts.OrderBy(m => m.ContractId).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.contracts.OrderByDescending(m => m.ContractId).ToList());
            }

            if (vendId <= 0 && equipId <= 0 && searchText == 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(_context.contracts);
            }
            if (vendId > 0 && equipId <= 0 && searchText == 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                contracts = _context.contracts.Where(c => c.VenId == vendId).ToList();
            }
            if (vendId <= 0 && equipId > 0 && searchText == 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                contracts = _context.contracts.Where(c => c.EquipmentId == equipId).ToList();
            }

            if (vendId <= 0 && equipId <= 0 && searchText > 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                contracts = _context.contracts.Where(c => c.ContractId == searchText).ToList();
            }

            return View(contracts);
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.contracts
                .Include(c => c.Equipment)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewBag.AllVendors = _context.Vendors.ToList();
            ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
            return View(); ;
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Contract contract)
        {
            if(ModelState.IsValid == true && contract.EquipmentId > 0)
            {
                Equipment equipment = _context.Equipments.Where(e => e.EqId == contract.EquipmentId).FirstOrDefault();
                Vendor vendor = _context.Vendors.Where(v => v.VendorId == equipment.VenId).FirstOrDefault();

                contract.VenId = vendor.VendorId;
                Contract contract1 = _context.contracts.Where(c => c.EquipmentId == contract.EquipmentId).FirstOrDefault();

                if (contract1 == null)                 // l if hena 3amelha 3shan ma3melsh add fil inventory le equipment akter mn mara, we lama l youer ye3ml keda hayefdal                                         fe nafs l page msh 7ayro7 7eta
                {
                    _context.Add(contract);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.AllVendors = _context.Vendors.ToList();
                    ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 

                    return View(contract);
                }
            }
            else
            {
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 

                return View(contract);
            }
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Contract contract = _context.contracts.Find(id);
            if (contract == null)
            {
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                return NotFound();
            }
            else
            {
                ViewBag.AllVendors = _context.Vendors.ToList();
                ViewBag.AllEquipments = _context.Equipments.ToList();  //search by equipment table 
                return View(contract);
            }
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contract contract)
        {
            contract.ContractId = id;


            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {

                Equipment equipment = _context.Equipments.Where(e => e.EqId == contract.EquipmentId).FirstOrDefault();
                Vendor vendor = _context.Vendors.Where(v => v.VendorId == equipment.VenId).FirstOrDefault();
                contract.VenId = vendor.VendorId;

                _context.contracts.Update(contract);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.contracts
                .Include(c => c.Equipment)
                .Include(c => c.Vendor)
                .FirstOrDefaultAsync(m => m.ContractId == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.contracts.FindAsync(id);
            _context.contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.contracts.Any(e => e.ContractId == id);
        }
    }
}
