using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Application.Services.Base
{
    public interface IPropertyTypeService
    {
        IEnumerable<PropertyType> GetAllPropertyTypes();
        PropertyType GetPropertyTypeById(int id);
        void AddPropertyType(PropertyType propertyType);
        void UpdatePropertyType(PropertyType propertyType);
        void DeletePropertyType(PropertyType propertyType);
    }
}
