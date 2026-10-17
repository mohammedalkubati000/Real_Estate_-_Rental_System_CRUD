using Microsoft.Identity.Client.NativeInterop;

namespace Real_Estate___Rental_System_CRUD.Services.Base
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(int id);
        void AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
    }
}
