using Real_Estate___Rental_System_CRUD.Dtos;
using Real_Estate___Rental_System_CRUD.Models;
using Real_Estate___Rental_System_CRUD.Repositories.Base;
using Real_Estate___Rental_System_CRUD.Services.Base;

namespace Real_Estate___Rental_System_CRUD.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PropertyDto>> GetAllProperties()
        {
            return await _unitOfWork.PropertyRepository.GetAllPropertiesAsync();
        }

        public async Task<IEnumerable<PropertyType>> GetAllPropertyTypes()
        {
            return await _unitOfWork.PropertyRepository.GetPropertyTypesAsync();
        }

        public async Task<Property?> GetPropertyByUuid(string uuid)
        {
            return await _unitOfWork.PropertyRepository.GetPropertyByUuidAsync(uuid);
        }

        public async Task AddProperty(PropertyCreateDto dto)
        {
            var property = new Property
            {
                PropertyTypeId = dto.PropertyTypeId,
                City = dto.City,
                Address = dto.Address,
                AnnualRent = dto.AnnualRent,
                Uuid = Guid.NewGuid().ToString()
            };

            await _unitOfWork.PropertyRepository.AddPropertyAsync(property);
        }

        public async Task UpdateProperty(PropertyUpdateDto dto)
        {
            var property = await _unitOfWork.PropertyRepository.GetPropertyByUuidAsync(dto.Uuid);
            if (property != null)
            {
                property.PropertyTypeId = dto.PropertyTypeId;
                property.City = dto.City;
                property.Address = dto.Address;
                property.AnnualRent = dto.AnnualRent;
                property.IsAvailable = dto.IsAvailable;

                await _unitOfWork.PropertyRepository.UpdatePropertyAsync(property);
            }
        }

        public async Task DeleteProperty(string uuid)
        {
            await _unitOfWork.PropertyRepository.DeletePropertyAsync(uuid);
        }
    }
}