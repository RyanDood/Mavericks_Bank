using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IAccountsAdminService:IAccountsUserService
    {
        public Task<Accounts> GetAccount(long accountNumber);
        public Task<List<Accounts>> GetAllAccounts();
        public Task<Accounts> UpdateAccountBalance(long accountNumber, double balance);
        public Task<Accounts> UpdateAccountStatus(long accountNumber, string status);
    }
}
