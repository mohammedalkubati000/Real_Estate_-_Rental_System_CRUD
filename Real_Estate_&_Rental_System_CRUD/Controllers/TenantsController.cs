using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
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
        public ActionResult Edit(int Id)
        {
            var tenant = _db.Tenants.Find(Id);
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
                _db.Tenants.Update(tenant);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(tenant);
        }


        //Delete


        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var tenant = _db.Tenants.Find(Id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var tenant = _db.Tenants.Find(Id);
            if (tenant == null)
            {
                return NotFound();
            }
                _db.Tenants.Remove(tenant);
                _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


}

