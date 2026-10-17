using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _db;

        public TenantRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await _db.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetTenantByUuidAsync(string uuid)
        {
            return await _db.Tenants.FirstOrDefaultAsync(t => t.Uuid == uuid);
        }

        public async Task AddTenantAsync(Tenant tenant)
        {
            await _db.Tenants.AddAsync(tenant);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTenantAsync(Tenant tenant)
        {
            var oldTenant = await _db.Tenants.FirstOrDefaultAsync(m => m.Uuid == tenant.Uuid);
            if (oldTenant != null)
            {
                oldTenant.FullName = tenant.FullName;
                oldTenant.NationalId = tenant.NationalId;
                oldTenant.PhoneNumber = tenant.PhoneNumber;
                oldTenant.Email = tenant.Email;

                _db.Tenants.Update(oldTenant);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteTenantAsync(string uuid)
        {
            var tenant = await _db.Tenants.FirstOrDefaultAsync(m => m.Uuid == uuid);
            if (tenant != null)
            {
                _db.Tenants.Remove(tenant);
                await _db.SaveChangesAsync();
            }
        }
    }
}