using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Services.Base
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetAllPayments();
        Payment GetPaymentById(int id);
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(Payment payment);
    }
}
