using Real_Estate___Rental_System_CRUD.Data;
using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            PropertyRepository = new PropertyRepository(_db);
            PropertyTypeRepository = new PropertyTypeRepository(_db);
            TenantRepository = new TenantRepository(_db);
            RentalContractRepository = new RentalContractRepository(_db);
            PaymentRepository = new PaymentRepository(_db);
            UserRepository = new UserRepository(_db);
        }











        public IPaymentRepository           PaymentRepository           { get; }

        public IPropertyRepository          PropertyRepository          { get; }

        public IPropertyTypeRepository      PropertyTypeRepository      { get; }

        public IRentalContractRepository    RentalContractRepository    { get; }

        public ITenantRepository            TenantRepository            { get; }

        public IUserRepository              UserRepository              { get; }
    }
}
