using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly AppDbContext _db;

        public PropertiesController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            IEnumerable<Property> properties = _db.Properties.ToList();
            return View(properties);
        }

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

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var property = _db.Properties.Find(Id);
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
                _db.Properties.Update(property);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(property);
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var property = _db.Properties.Find(Id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        public ActionResult Delete(Property property)
        {
            _db.Properties.Remove(property);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

