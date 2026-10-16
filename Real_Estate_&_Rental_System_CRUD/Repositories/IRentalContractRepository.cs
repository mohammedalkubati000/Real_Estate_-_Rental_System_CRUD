using Real_Estate___Rental_System_CRUD.Models;

namespace Real_Estate___Rental_System_CRUD.Repositories
{
    public interface IRentalContractRepository
    {
        Task<IEnumerable<RentalContract>> GetAllContractsAsync();
        Task<RentalContract?> GetContractByUuidAsync(string uuid);
        Task AddContractAsync(RentalContract contract);
        Task DeleteContractAsync(string uuid);
    }
}
