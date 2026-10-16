using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Repositories
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> GetAllTenantsAsync();
        Task<Tenant?> GetTenantByUuidAsync(string uuid);
        Task AddTenantAsync(Tenant tenant);
        Task UpdateTenantAsync(Tenant tenant);
        Task DeleteTenantAsync(string uuid);
    }
}
