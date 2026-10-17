using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Services.Base
{
    public interface IRentalContractService
    {
        IEnumerable<RentalContract> GetAllRentalContracts();
        RentalContract GetRentalContractById(int id);
        void AddRentalContract(RentalContract rentalContract);
        void UpdateRentalContract(RentalContract rentalContract);
        void DeleteRentalContract(RentalContract rentalContract);
    }
}
