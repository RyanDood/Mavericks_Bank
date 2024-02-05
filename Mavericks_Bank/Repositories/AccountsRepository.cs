using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class AccountsRepository : IRepository<long, Accounts>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<AccountsRepository> _loggerAccountsRepository;

        public AccountsRepository(MavericksBankContext mavericksBankContext, ILogger<AccountsRepository> loggerAccountsRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerAccountsRepository = loggerAccountsRepository;
        }

        public async Task<Accounts> Add(Accounts item)
        {
            _mavericksBankContext.Accounts.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerAccountsRepository.LogInformation($"Added New Account : {item.AccountNumber}");
            return item;
        }

        public async Task<Accounts?> Delete(long key)
        {
            var foundedAccount = await Get(key);
            if (foundedAccount == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Accounts.Remove(foundedAccount);
                await _mavericksBankContext.SaveChangesAsync();
                return foundedAccount;
            }
        }

        public async Task<Accounts?> Get(long key)
        {
            var foundedAdmin = await _mavericksBankContext.Accounts.FirstOrDefaultAsync(account => account.AccountNumber == key);
            if (foundedAdmin == null)
            {
                return null;
            }
            else
            {
                return foundedAdmin;
            }
        }

        public async Task<List<Accounts>?> GetAll()
        {
            var allAccounts = await _mavericksBankContext.Accounts.ToListAsync();
            if (allAccounts.Count == 0)
            {
                return null;
            }
            else
            {
                return allAccounts;
            }
        }

        public async Task<Accounts> Update(Accounts item)
        {
            _mavericksBankContext.Entry<Accounts>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            return item;
        }
    }
}
