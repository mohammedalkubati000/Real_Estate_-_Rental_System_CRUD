using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
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
        public ActionResult Delete(int Id)
        {
            var contract = _db.RentalContracts
                .Include(c => c.Property)
                .Include(c => c.Tenant)
                .FirstOrDefault(c => c.Id == Id);

            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var contract = _db.RentalContracts.Find(Id);
            if (contract != null)
            {

                var property = _db.Properties.Find(contract.PropertyId);
                if (property != null) property.IsAvailable = true;

                _db.RentalContracts.Remove(contract);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}

