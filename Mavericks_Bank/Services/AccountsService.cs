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
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<AccountsService> _loggerAccountsService;

        public AccountsService(IRepository<long, Accounts> accountsRepository, ICustomersAdminService customersService, ILogger<AccountsService> loggerAccountsService)
        {
            _accountsRepository = accountsRepository;
            _customersService = customersService;
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

        public async Task<List<Accounts>> GetAllCustomerAccounts(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAccounts = await GetAllAccounts();
            var allCustomerAccounts = allAccounts.Where(account => account.CustomerID == customerID).ToList();
            if(allCustomerAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No Accounts Available for Customer ID {customerID}");
            }
            return allCustomerAccounts;
        }

        public async Task<Accounts> UpdateAccountBalance(long accountNumber, double balance)
        {
            var foundedAccount = await GetAccount(accountNumber);
            foundedAccount.Balance = balance;
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }

        public async Task<Accounts> UpdateAccountStatus(long accountNumber, string status)
        {
            var foundedAccount = await GetAccount(accountNumber);
            foundedAccount.Status = status;
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }
    }
}
