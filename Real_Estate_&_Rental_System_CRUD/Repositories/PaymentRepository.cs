using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _db;

        public PaymentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _db.Payments
                .Include(p => p.RentalContract)
                .ThenInclude(c => c.Tenant)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _db.Payments
                .Include(p => p.RentalContract)
                .ThenInclude(c => c.Tenant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> GetPaymentByUuidAsync(string uuid)
        {
            return await _db.Payments
                .Include(p => p.RentalContract)
                .ThenInclude(c => c.Tenant)
                .FirstOrDefaultAsync(p => p.Uuid == uuid);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            var oldPayment = await _db.Payments.FirstOrDefaultAsync(m => m.Uuid == payment.Uuid);
            if (oldPayment != null)
            {
                oldPayment.RentalContractId = payment.RentalContractId;
                oldPayment.AmountPaid = payment.AmountPaid;
                oldPayment.PaymentDate = payment.PaymentDate;
                oldPayment.PaymentMethod = payment.PaymentMethod;
                oldPayment.Status = payment.Status;

                _db.Payments.Update(oldPayment);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _db.Payments.FirstOrDefaultAsync(p => p.Id == id);
            if (payment != null)
            {
                _db.Payments.Remove(payment);
                await _db.SaveChangesAsync();
            }
        }
    }
}