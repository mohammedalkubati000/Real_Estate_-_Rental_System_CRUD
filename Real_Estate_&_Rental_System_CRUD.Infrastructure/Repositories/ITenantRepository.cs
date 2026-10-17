using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
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
