using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Services.Base
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyDto>> GetAllProperties();
        Task<IEnumerable<PropertyType>> GetAllPropertyTypes();
        Task<Property?> GetPropertyByUuid(string uuid);
        Task AddProperty(PropertyCreateDto dto);
        Task UpdateProperty(PropertyUpdateDto dto);
        Task DeleteProperty(string uuid);
    }
}
