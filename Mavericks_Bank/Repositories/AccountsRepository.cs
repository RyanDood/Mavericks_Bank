﻿using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class AccountsRepository : IRepository<int, Accounts>
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
            _loggerAccountsRepository.LogInformation($"Added New Account : {item.AccountID}");
            return item;
        }

        public async Task<Accounts?> Delete(int key)
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
                _loggerAccountsRepository.LogInformation($"Deleted Account : {foundedAccount.AccountID}");
                return foundedAccount;
            }
        }

        public async Task<Accounts?> Get(int key)
        {
            var foundedAccount = await _mavericksBankContext.Accounts.Include(account => account.Branches).ThenInclude(branch => branch!.Banks).Include(account => account.Customers).FirstOrDefaultAsync(account => account.AccountID == key);
            if (foundedAccount == null)
            {
                return null;
            }
            else
            {
                _loggerAccountsRepository.LogInformation($"Founded Account : {foundedAccount.AccountID}");
                return foundedAccount;
            }
        }

        public async Task<List<Accounts>?> GetAll()
        {
            var allAccounts = await _mavericksBankContext.Accounts.Include(account => account.Branches).ThenInclude(branch => branch!.Banks).Include(account => account.Customers).ToListAsync();
            if (allAccounts.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerAccountsRepository.LogInformation("All Accounts Returned");
                return allAccounts;
            }
        }

        public async Task<Accounts> Update(Accounts item)
        {
            _mavericksBankContext.Entry<Accounts>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerAccountsRepository.LogInformation($"Updated Account : {item.AccountID}");
            return item;
        }
    }
}
