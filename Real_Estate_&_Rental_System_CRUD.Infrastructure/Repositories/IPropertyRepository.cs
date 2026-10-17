//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Real_Estate___Rental_System_CRUD.Domain.Models;
namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByUuidAsync(string uuid);
        Task AddPropertyAsync(Property property);
        Task UpdatePropertyAsync(Property property);
        Task DeletePropertyAsync(string uuid);
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}
