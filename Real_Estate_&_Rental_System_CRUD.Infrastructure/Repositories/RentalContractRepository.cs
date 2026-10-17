using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Real_Estate___Rental_System_CRUD.Domain.Models;

namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories
{
    public class RentalContractRepository : IRentalContractRepository
    {
        private readonly AppDbContext _db;

        public RentalContractRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RentalContract>> GetAllContractsAsync()
        {
            return await _db.RentalContracts
                .Include(r => r.Property)
                .Include(r => r.Tenant)
                .ToListAsync();
        }

        public async Task<RentalContract?> GetContractByUuidAsync(string uuid)
        {
            return await _db.RentalContracts
                .Include(r => r.Property)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(r => r.Uuid == uuid);
        }

        public async Task AddContractAsync(RentalContract contract)
        {
            await _db.RentalContracts.AddAsync(contract);

            // تحديث حالة العقار ليصبح غير متاح عند توقيع العقد
            var property = await _db.Properties.FindAsync(contract.PropertyId);
            if (property != null)
            {
                property.IsAvailable = false;
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeleteContractAsync(string uuid)
        {
            var contract = await _db.RentalContracts.FirstOrDefaultAsync(m => m.Uuid == uuid);
            if (contract != null)
            {
                // إرجاع حالة العقار ليصبح متاحاً عند حذف/فسخ العقد
                var property = await _db.Properties.FindAsync(contract.PropertyId);
                if (property != null)
                {
                    property.IsAvailable = true;
                }

                _db.RentalContracts.Remove(contract);
                await _db.SaveChangesAsync();
            }
        }
    }
}