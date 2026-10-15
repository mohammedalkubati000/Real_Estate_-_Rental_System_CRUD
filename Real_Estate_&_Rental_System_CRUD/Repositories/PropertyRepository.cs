using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;

        public PropertyRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task AddPropertyAsync(Real_Estate___Rental_System_CRUD.Models.Property property)
        {
            _db.Properties.Add(property);
            return _db.SaveChangesAsync();
            
        }
        public Task DeletePropertyAsync(string uuid)
        {
            var property = _db.Properties.FirstOrDefault(p => p.Uuid == uuid);
            if (property != null)
            {
                _db.Properties.Remove(property);
                return _db.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            IEnumerable<Property> properties = await _db.Properties
                .Include(p => p.PropertyType)
                .ToListAsync();

            return properties;
        }

        public async Task<Property?> GetPropertyByUuidAsync(string uuid)
        {
            var property = await _db.Properties.FirstOrDefaultAsync(p => p.Uuid == uuid);
            return property;
        }

        public async Task UpdatePropertyAsync(Real_Estate___Rental_System_CRUD.Models.Property property)
        {
            _db.Properties.Update(property);
            await _db.SaveChangesAsync();
        }
        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await _db.PropertyType.ToListAsync();
        }
    }
}
