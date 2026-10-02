using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class RentalContractsController : Controller
    {
        private readonly AppDbContext _db;

        public RentalContractsController(AppDbContext db)
        {
            _db = db;
        }

        //////

        //Index

        //////

        public ActionResult Index()
        {

            IEnumerable<RentalContract> contracts = _db.RentalContracts
                .Include(c => c.Property)
                .Include(c => c.Tenant)
                .ToList();
            return View(contracts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(RentalContract contract)
        {
            if (ModelState.IsValid)
            {
                _db.RentalContracts.Add(contract);

                var property = _db.Properties.Find(contract.PropertyId);
                if (property != null) property.IsAvailable = false;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(contract);
        }
        private void LoadDropdowns()
        {
            ViewBag.PropertyId = new SelectList(_db.Properties.Where(p => p.IsAvailable), "Id", "Address");
            ViewBag.TenantId = new SelectList(_db.Tenants, "Id", "FullName");
        }




        //===============
        //Delete
        //===============
        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid)) return NotFound();

            var contract = _db.RentalContracts
                .Include(c => c.Property)
                .Include(c => c.Tenant)
                .FirstOrDefault(c => c.Uuid == Uuid);

            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        [HttpPost]
        [ActionName("Delete")] 
        public ActionResult DeleteConfirmed(RentalContract rentalContract)
        {
            var oldCon = _db.RentalContracts.FirstOrDefault(m => m.Uuid == rentalContract.Uuid);
            if (oldCon != null)
            {

                var property = _db.Properties.Find(oldCon.PropertyId);
                if (property != null) property.IsAvailable = true;

                _db.RentalContracts.Remove(oldCon);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}

