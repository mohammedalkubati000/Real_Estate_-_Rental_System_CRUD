using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly AppDbContext _db;

        public TenantsController(AppDbContext db)
        {
            _db = db;
        }

        //////

        //Index

        //////


        public ActionResult Index()
        {
            IEnumerable<Tenant> tenants = _db.Tenants.ToList();
            return View(tenants);
        }

        //Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _db.Tenants.Add(tenant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(" ", "Please fill all the required fields.");
            return View(tenant);
        }


        //Edit


        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
            var tenant = _db.Tenants.FirstOrDefault(m => m.Uuid == Uuid);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        [HttpPost]
        public ActionResult Edit(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                var oldTenant = _db.Tenants.FirstOrDefault(m => m.Uuid == tenant.Uuid);
                if(oldTenant == null)
                
                    return NotFound();
                

                oldTenant.FullName = tenant.FullName;
                oldTenant.NationalId = tenant.NationalId;
                oldTenant.PhoneNumber = tenant.PhoneNumber;
                oldTenant.Email = tenant.Email;

                
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(tenant);
        }


        //Delete


        [HttpGet]
        public ActionResult Delete(string Uuid )
        {
            var tenant = _db.Tenants.FirstOrDefault(m => m.Uuid == Uuid);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(Tenant tenant)
        {
            var olddept = _db.Tenants.FirstOrDefault(m => m.Uuid == tenant.Uuid);
            if (olddept == null)

                return NotFound();
            
                _db.Tenants.Remove(tenant);
                _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


}

