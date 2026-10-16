using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories;
using Real_Estate___Rental_System_CRUD.Repositories.Base;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class RentalContractsController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public RentalContractsController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === Old Repository Pattern Injection (Commented Out) ===
        // private readonly IRentalContractRepository _contractRepository;
        // private readonly IPropertyRepository _propertyRepository;
        // private readonly ITenantRepository _tenantRepository;

        // public RentalContractsController(
        //      IRentalContractRepository contractRepository,
        //      IPropertyRepository propertyRepository,
        //      ITenantRepository tenantRepository)
        //  {
        //       _contractRepository = contractRepository;
        //     _propertyRepository = propertyRepository;
        //       _tenantRepository = tenantRepository;
        //   }

        // === New UnitOfWork Injection ===
        private readonly IUnitOfWork _unitOfWork;

        public RentalContractsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: RentalContracts
        public async Task<IActionResult> Index()
        {
            // === Old DbContext Code (Commented Out) ===
            // IEnumerable<RentalContract> contracts = _db.RentalContracts
            //     .Include(c => c.Property)
            //     .Include(c => c.Tenant)
            //     .ToList();
            // return View(contracts);

            // === New Repository Code via UnitOfWork ===
            var contracts = await _unitOfWork.RentalContractRepository.GetAllContractsAsync();
            return View(contracts);
        }

        // GET: RentalContracts/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        // POST: RentalContracts/Create
        [HttpPost]
        public async Task<IActionResult> Create(RentalContract contract)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // _db.RentalContracts.Add(contract);
                // var property = _db.Properties.Find(contract.PropertyId);
                // if (property != null) property.IsAvailable = false;
                // _db.SaveChanges();

                // === New Repository Code via UnitOfWork ===
                await _unitOfWork.RentalContractRepository.AddContractAsync(contract);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownsAsync();
            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(contract);
        }

        // GET: RentalContracts/Delete/{Uuid}
        [HttpGet]
        public async Task<IActionResult> Delete(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid)) return NotFound();

            // === Old DbContext Code (Commented Out) ===
            // var contract = _db.RentalContracts
            //     .Include(c => c.Property)
            //     .Include(c => c.Tenant)
            //     .FirstOrDefault(c => c.Uuid == Uuid);

            // === New Repository Code via UnitOfWork ===
            var contract = await _unitOfWork.RentalContractRepository.GetContractByUuidAsync(Uuid);

            if (contract == null)
            {
                return NotFound();
            }
            return View(contract);
        }

        // POST: RentalContracts/Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(RentalContract rentalContract)
        {
            // === Old DbContext Code (Commented Out) ===
            // var oldCon = _db.RentalContracts.FirstOrDefault(m => m.Uuid == rentalContract.Uuid);
            // if (oldCon != null)
            // {
            //     var property = _db.Properties.Find(oldCon.PropertyId);
            //     if (property != null) property.IsAvailable = true;
            //     _db.RentalContracts.Remove(oldCon);
            //     _db.SaveChanges();
            // }

            // === New Repository Code via UnitOfWork ===
            if (!string.IsNullOrEmpty(rentalContract.Uuid))
            {
                await _unitOfWork.RentalContractRepository.DeleteContractAsync(rentalContract.Uuid);
            }

            return RedirectToAction(nameof(Index));
        }

        // === Helper Method for Dropdowns ===
        private async Task LoadDropdownsAsync()
        {
            // === Old DbContext Code (Commented Out) ===
            // var props = _db.Properties
            // .Include(p => p.PropertyType)
            // .Where(p => p.IsAvailable)
            // .Select(p => new
            // {
            //      p.Id,
            //      Text = p.PropertyType!.Name + " - " + p.City + " - " + p.Address
            // })
            // .ToList();
            // ViewBag.PropertyId = new SelectList(props, "Id", "Text");
            // ViewBag.TenantId = new SelectList(_db.Tenants, "Id", "FullName");

            // === New Repository Code via UnitOfWork ===
            var allProperties = await _unitOfWork.PropertyRepository.GetAllPropertiesAsync();
            var availableProperties = allProperties
                .Where(p => p.IsAvailable)
                .Select(p => new
                {
                    p.Id,
                    Text = (p.PropertyType != null ? p.PropertyType.Name : "") + " - " + p.City + " - " + p.Address
                });

            var tenants = await _unitOfWork.TenantRepository.GetAllTenantsAsync();

            ViewBag.PropertyId = new SelectList(availableProperties, "Id", "Text");
            ViewBag.TenantId = new SelectList(tenants, "Id", "FullName");
        }
    }
}