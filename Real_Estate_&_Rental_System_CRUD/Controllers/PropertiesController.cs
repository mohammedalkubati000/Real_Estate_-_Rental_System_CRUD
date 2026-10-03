using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class PropertiesController : Controller
    {
        private readonly AppDbContext _db;

        public PropertiesController(AppDbContext db)
        {
            _db = db;
        }

        //////

        //Index

        //////
        public ActionResult Index()
        {
            IEnumerable<Property> properties = _db.Properties.ToList();
            return View(properties);
            
        }


        //Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Property property)
        {
            if (ModelState.IsValid)
            {
                _db.Properties.Add(property);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(property);
        }


        //Edit


        [HttpGet]
        public ActionResult Edit(string Uuid)
        {
                var property = _db.Properties.FirstOrDefault(m => m.Uuid == Uuid);
            if (property == null)
                {
                    return NotFound();
                }
                return View(property);
        }

        [HttpPost]
        public ActionResult Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                var oldProperty = _db.Properties.FirstOrDefault(m => m.Uuid == property.Uuid);
                
                if (oldProperty == null)

                    return NotFound();
                oldProperty.PropertyType = property.PropertyType;
                oldProperty.City = property.City;
                oldProperty.Address = property.Address;
                oldProperty.AnnualRent = property.AnnualRent;

               
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(property);
        }


        //Delete


        [HttpGet]
        public ActionResult Delete(string Uuid)
        {
            var property = _db.Properties.FirstOrDefault(m => m.Uuid == Uuid);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(Property property)
        {
            var oldprop = _db.Properties.FirstOrDefault(m => m.Uuid == property.Uuid);
            if (oldprop == null)
            {
                return NotFound();
            }
            _db.Properties.Remove(oldprop);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

