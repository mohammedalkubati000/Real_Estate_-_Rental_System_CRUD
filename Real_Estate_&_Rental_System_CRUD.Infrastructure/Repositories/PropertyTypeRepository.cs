using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
{
    public class PropertyTypeRepository : IPropertyTypeRepository
    {
        private readonly AppDbContext _db;

        public PropertyTypeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync()
        {
            return await _db.PropertyType.ToListAsync();
        }

        public async Task<PropertyType?> GetPropertyTypeByIdAsync(int id)
        {
            return await _db.PropertyType.FirstOrDefaultAsync(pt => pt.Id == id);
        }

        public async Task AddPropertyTypeAsync(PropertyType propertyType)
        {
            _db.PropertyType.Add(propertyType);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePropertyTypeAsync(PropertyType propertyType)
        {
            _db.PropertyType.Update(propertyType);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePropertyTypeAsync(int id)
        {
            var propertyType = await _db.PropertyType.FirstOrDefaultAsync(pt => pt.Id == id);
            if (propertyType != null)
            {
                _db.PropertyType.Remove(propertyType);
                await _db.SaveChangesAsync();
            }
        }
    }
}