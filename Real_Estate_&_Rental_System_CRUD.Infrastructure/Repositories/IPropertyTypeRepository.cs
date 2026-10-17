using Real_Estate___Rental_System_CRUD.Domain.Models;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync();
        Task<PropertyType?> GetPropertyTypeByIdAsync(int id);
        Task AddPropertyTypeAsync(PropertyType propertyType);
        Task UpdatePropertyTypeAsync(PropertyType propertyType);
        Task DeletePropertyTypeAsync(int id);
    }
}
