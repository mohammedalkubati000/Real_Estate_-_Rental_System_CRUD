using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories;

namespace Real_Estate___Rental_System_CRUD.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        // === Old DbContext Reference (Commented Out) ===
        // private readonly AppDbContext _db;
        // public TenantsController(AppDbContext db)
        // {
        //     _db = db;
        // }

        // === New Repository Pattern Injection ===
        private readonly ITenantRepository _tenantRepository;

        public TenantsController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            // === Old DbContext Code (Commented Out) ===
            // IEnumerable<Tenant> tenants = _db.Tenants.ToList();
            // return View(tenants);

            // === New Repository Code ===
            var tenants = await _tenantRepository.GetAllTenantsAsync();
            return View(tenants);
        }

        // GET: Tenants/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        [HttpPost]
        public async Task<IActionResult> Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // _db.Tenants.Add(tenant);
                // _db.SaveChanges();

                // === New Repository Code ===
                await _tenantRepository.AddTenantAsync(tenant);
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(tenant);
        }

        // GET: Tenants/Edit/{Uuid}
        [HttpGet]
        public async Task<IActionResult> Edit(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid))
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var tenant = _db.Tenants.FirstOrDefault(m => m.Uuid == Uuid);

            // === New Repository Code ===
            var tenant = await _tenantRepository.GetTenantByUuidAsync(Uuid);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                // === Old DbContext Code (Commented Out) ===
                // var oldTenant = _db.Tenants.FirstOrDefault(m => m.Uuid == tenant.Uuid);
                // if(oldTenant == null)
                //     return NotFound();
                // oldTenant.FullName = tenant.FullName;
                // oldTenant.NationalId = tenant.NationalId;
                // oldTenant.PhoneNumber = tenant.PhoneNumber;
                // oldTenant.Email = tenant.Email;
                // _db.SaveChanges();

                // === New Repository Code ===
                var existingTenant = await _tenantRepository.GetTenantByUuidAsync(tenant.Uuid);
                if (existingTenant == null)
                {
                    return NotFound();
                }

                await _tenantRepository.UpdateTenantAsync(tenant);
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Please fill all the required fields.");
            return View(tenant);
        }

        // GET: Tenants/Delete/{Uuid}
        [HttpGet]
        public async Task<IActionResult> Delete(string Uuid)
        {
            if (string.IsNullOrEmpty(Uuid))
            {
                return NotFound();
            }

            // === Old DbContext Code (Commented Out) ===
            // var tenant = _db.Tenants.FirstOrDefault(m => m.Uuid == Uuid);

            // === New Repository Code ===
            var tenant = await _tenantRepository.GetTenantByUuidAsync(Uuid);

            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Tenant tenant)
        {
            // === Old DbContext Code (Commented Out) ===
            // var olddept = _db.Tenants.FirstOrDefault(m => m.Uuid == tenant.Uuid);
            // if (olddept == null)
            //     return NotFound();
            // _db.Tenants.Remove(olddept);
            // _db.SaveChanges();

            // === New Repository Code ===
            var existingTenant = await _tenantRepository.GetTenantByUuidAsync(tenant.Uuid);
            if (existingTenant == null)
            {
                return NotFound();
            }

            await _tenantRepository.DeleteTenantAsync(tenant.Uuid);
            return RedirectToAction(nameof(Index));
        }
    }
}