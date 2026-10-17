using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Real_Estate___Rental_System_CRUD.Domain.Models;
using Real_Estate___Rental_System_CRUD.Infrastructure.Repositories;
using Real_Estate___Rental_System_CRUD.Infrastructure.Repositories.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public PaymentsController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IPaymentRepository _paymentRepository;
        // private readonly IRentalContractRepository _contractRepository;

        // public PaymentsController(IPaymentRepository paymentRepository, IRentalContractRepository contractRepository)
        // {
        //     _paymentRepository = paymentRepository;
        //     _contractRepository = contractRepository;
        // }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            // === Old DbContext Code (Commented Out) ===
            // IEnumerable<Payment> payments = _db.Payments.Include(p => p.RentalContract)
            // .ThenInclude(c => c.Tenant).ToList();
            // return View(payments);

            // === Old Repository Code (Commented Out) ===
            // var payments = await _paymentRepository.GetAllPaymentsAsync();

            // === New Repository Code via UnitOfWork ===
            var payments = await _unitOfWork.PaymentRepository.GetAllPaymentsAsync();
            return View(payments);
        }

        // GET: Payments/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // === Old DbContext Code (Commented Out) ===
            // IEnumerable<RentalContract> contracts = _db.RentalContracts
            // .Include(c => c.Tenant)
            // .ToList();
            // ViewBag.RentalContractId = new SelectList(contracts, "Id", "DisplayName");

            // === New Repository Code via UnitOfWork ===
            var contracts = await _unitOfWork.RentalContractRepository.GetAllContractsAsync();
            ViewBag.RentalContractId = new SelectList(contracts, "Id", "DisplayName");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // _db.Payments.Add(payment);
                // _db.SaveChanges();

                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.PaymentRepository.AddPaymentAsync(payment);
                return RedirectToAction(nameof(Index));
            }

            var contracts = await _unitOfWork.RentalContractRepository.GetAllContractsAsync();
            ViewBag.RentalContractId = new SelectList(contracts, "Id", "DisplayName");

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(payment);
        }

        // GET: Payments/Edit/{Uuid}
        [HttpGet]
        public async Task<IActionResult> Edit(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid)) return NotFound();

            // === Old DbContext Code (Commented Out) ===
            // var payment = _db.Payments.FirstOrDefault(m => m.Uuid == Uuid);

            // === New Repository Code via UnitOfWork ===
            var payment = await _unitOfWork.PaymentRepository.GetPaymentByUuidAsync(Uuid);

            if (payment == null)
            {
                return NotFound();
            }

            var contracts = await _unitOfWork.RentalContractRepository.GetAllContractsAsync();
            ViewBag.RentalContractId = new SelectList(contracts, "Id", "DisplayName", payment.RentalContractId);

            return View(payment);
        }

        // POST: Payments/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // var oldPayment = _db.Payments.FirstOrDefault(m => m.Uuid == payment.Uuid);
                // if (oldPayment == null)
                //     return NotFound();
                // oldPayment.RentalContractId = payment.RentalContractId; 
                // oldPayment.AmountPaid = payment.AmountPaid;
                // oldPayment.PaymentDate = payment.PaymentDate;
                // oldPayment.PaymentMethod = payment.PaymentMethod;
                // oldPayment.Status = payment.Status;
                // _db.SaveChanges();

                // === New Repository Code via UnitOfWork ===
                var oldPayment = await _unitOfWork.PaymentRepository.GetPaymentByUuidAsync(payment.Uuid);
                if (oldPayment == null)
                {
                    return NotFound();
                }

                await _unitOfWork.PaymentRepository.UpdatePaymentAsync(payment);
                return RedirectToAction(nameof(Index));
            }

            var contracts = await _unitOfWork.RentalContractRepository.GetAllContractsAsync();
            ViewBag.RentalContractId = new SelectList(contracts, "Id", "DisplayName", payment.RentalContractId);

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(payment);
        }

        // GET: Payments/Delete/{Id}
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var payment = _db.Payments
            //     .Include(p => p.RentalContract)
            //     .ThenInclude(c => c.Tenant)
            //     .FirstOrDefault(p => p.Id == Id);

            // === New Repository Code via UnitOfWork ===
            var payment = await _unitOfWork.PaymentRepository.GetPaymentByIdAsync(Id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/{Id}
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            // === Old DbContext Code (Commented Out) ===
            // var payment = _db.Payments.Find(Id);
            // if (payment != null)
            // {
            //     _db.Payments.Remove(payment);
            //     _db.SaveChanges();
            // }

            // === New Repository Code via UnitOfWork ===
            await _unitOfWork.PaymentRepository.DeletePaymentAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}