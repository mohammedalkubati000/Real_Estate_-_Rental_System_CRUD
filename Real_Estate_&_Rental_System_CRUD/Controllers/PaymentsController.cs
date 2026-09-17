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
           
            IEnumerable<Payment> payments = _db.Payments
                .Include(p => p.RentalContract)
                .ThenInclude(c => c.Tenant)
                .ToList();
            return View(payments);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            var contractsList = _db.RentalContracts.Include(c => c.Tenant).ToList().Select(c => new
            {
                Id = c.Id,
                DisplayText = $"عقد رقم {c.Id} - {c.Tenant?.FullName}"
            });

            ViewBag.RentalContractId = new SelectList(contractsList, "Id", "DisplayText");
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

            var contractsList = _db.RentalContracts.Include(c => c.Tenant).ToList().Select(c => new { Id = c.Id, DisplayText = $"عقد رقم {c.Id} - {c.Tenant?.FullName}" });
            ViewBag.RentalContractId = new SelectList(contractsList, "Id", "DisplayText", payment.RentalContractId);
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(payment);
        }
    }

}

