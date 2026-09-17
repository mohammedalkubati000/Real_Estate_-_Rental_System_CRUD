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

        public ActionResult Index()
        {
           
            IEnumerable<RentalContract> contracts = _db.RentalContracts
                .Include(c => c.Property)
                .Include(c => c.Tenant)
                .ToList();
            return View("~/Views/RentalContracts/Index.cshtml", contracts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            ViewBag.PropertyId = new SelectList(_db.Properties.Where(p => p.IsAvailable).ToList(), "Id", "Address");
            ViewBag.TenantId = new SelectList(_db.Tenants.ToList(), "Id", "FullName");
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

            ViewBag.PropertyId = new SelectList(_db.Properties.Where(p => p.IsAvailable).ToList(), "Id", "Address", contract.PropertyId);
            ViewBag.TenantId = new SelectList(_db.Tenants.ToList(), "Id", "FullName", contract.TenantId);
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(contract);
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
        public ActionResult Delete(RentalContract contract)
        {
            var originalContract = _db.RentalContracts.Find(contract.Id);
            if (originalContract != null)
            {
                
                var property = _db.Properties.Find(originalContract.PropertyId);
                if (property != null) property.IsAvailable = true;

                _db.RentalContracts.Remove(originalContract);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

