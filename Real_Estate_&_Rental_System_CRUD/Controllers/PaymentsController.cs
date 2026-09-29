using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    public class PaymentsController : Controller
    {

        private readonly AppDbContext _db;

        public PaymentsController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {

            IEnumerable<Payment> payments = _db.Payments.Include(p => p.RentalContract)
            .ThenInclude(c => c.Tenant).ToList();
            return View(payments);
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _db.Payments.Add(payment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(payment);

        }

      
        //Edit
        
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var payment = _db.Payments.Find(Id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost]
        public ActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _db.Payments.Update(payment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(payment);

        }

        //Delete

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var payment = _db.Payments
                .Include(p => p.RentalContract)
                .ThenInclude(c => c.Tenant)
                .FirstOrDefault(p => p.Id == Id);
            if (payment == null)
            {
                return NotFound();
            }

                return View(payment);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            var payment = _db.Payments.Find(Id);
            if (payment != null)
            {
                _db.Payments.Remove(payment);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");


        }







    }

}

