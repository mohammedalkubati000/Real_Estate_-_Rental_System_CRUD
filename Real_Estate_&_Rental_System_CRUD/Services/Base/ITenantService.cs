using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Services.Base
{
    public interface ITenantService
    {
        IEnumerable<Tenant> GetAllTenants();
        Tenant GetTenantById(int id);
        void AddTenant(Tenant tenant);
        void UpdateTenant(Tenant tenant);
        void DeleteTenant(Tenant tenant);
    }
}
