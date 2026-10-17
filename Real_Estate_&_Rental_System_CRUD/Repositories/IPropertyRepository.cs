//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Models;
namespace Real_Estate___Rental_System_CRUD.Repositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
        Task<Property?> GetPropertyByUuidAsync(string uuid);
        Task AddPropertyAsync(Property property);
        Task UpdatePropertyAsync(Property property);
        Task DeletePropertyAsync(string uuid);
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}
