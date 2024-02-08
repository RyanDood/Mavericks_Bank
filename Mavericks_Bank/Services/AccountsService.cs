using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class AccountsService : IAccountsAdminService
    {
        private readonly IRepository<long,Accounts> _accountsRepository;
        private readonly ILogger<AccountsService> _loggerAccountsService;

        public AccountsService(IRepository<long, Accounts> accountsRepository, ILogger<AccountsService> loggerAccountsService)
        {
            _accountsRepository = accountsRepository;
            _loggerAccountsService = loggerAccountsService;
        }

        public async Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO)
        {
            Accounts newAccount = new ConvertToAccounts(addNewAccountDTO).GetAccount();
            return await _accountsRepository.Add(newAccount);
        }

        public async Task<Accounts> DeleteAccount(long accountNumber)
        {
            var deletedAccount = await _accountsRepository.Delete(accountNumber);
            if(deletedAccount == null) 
            {
                throw new NoAccountsFoundException($"Account Number {accountNumber} not found");
            }
            return deletedAccount;
        }

        public async Task<Accounts> GetAccount(long accountNumber)
        {
            var foundedAccount = await _accountsRepository.Get(accountNumber);
            if (foundedAccount == null)
            {
                throw new NoAccountsFoundException($"Account Number {accountNumber} not found");
            }
            return foundedAccount;
        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            var allAccounts = await _accountsRepository.GetAll();
            if(allAccounts == null)
            {
                throw new NoAccountsFoundException("No Available Accounts Data");
            }
            return allAccounts;
        }

        public async Task<Accounts> UpdateAccountBalance(UpdateAccountBalanceDTO updateAccountBalanceDTO)
        {
            var foundedAccount = await GetAccount(updateAccountBalanceDTO.AccountNumber);
            foundedAccount.Balance = updateAccountBalanceDTO.Balance;
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }
    }
}
