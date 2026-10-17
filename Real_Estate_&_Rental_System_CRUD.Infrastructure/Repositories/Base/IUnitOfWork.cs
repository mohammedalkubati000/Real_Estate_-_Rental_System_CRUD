namespace Real_Estate___Rental_System_CRUD.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork
    {
        IPaymentRepository          PaymentRepository           { get; }

        IPropertyRepository         PropertyRepository          { get; }

        IPropertyTypeRepository     PropertyTypeRepository      { get; }

        IRentalContractRepository   RentalContractRepository    { get; }

        ITenantRepository           TenantRepository            { get; }

        IUserRepository             UserRepository              { get; }


    }
}
