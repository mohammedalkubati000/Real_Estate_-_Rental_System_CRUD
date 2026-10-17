using Microsoft.AspNetCore.Identity;
using Real_Estate___Rental_System_CRUD.Domain.Models;
namespace Real_Estate___Rental_System_CRUD.Application.Services.Base
{
    public interface IAccountService
    {
        IEnumerable<User> GetAllAccounts();
        User GetAccountById(int id);
        void AddAccount(User account);
        void UpdateAccount(User account);
        void DeleteAccount(User account);
    
    }
}
