using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using System.Net.NetworkInformation;

namespace Mavericks_Bank.Services
{
    public class AccountsService : IAccountsAdminService
    {
        private readonly IRepository<int,Accounts> _accountsRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<AccountsService> _loggerAccountsService;

        public AccountsService(IRepository<int, Accounts> accountsRepository, ICustomersAdminService customersService, ILogger<AccountsService> loggerAccountsService)
        {
            _accountsRepository = accountsRepository;
            _customersService = customersService;
            _loggerAccountsService = loggerAccountsService;
        }

        public async Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO)
        {
            var allAccounts = await _accountsRepository.GetAll();
            var foundedAccount = allAccounts?.FirstOrDefault(account => account.AccountType == addNewAccountDTO.AccountType && account.CustomerID == addNewAccountDTO.CustomerID);
            if (foundedAccount != null)
            {
                throw new AccountNumberAlreadyExistsException($"Account Type {addNewAccountDTO.AccountType} already exists");
            }
            Accounts newAccount = new ConvertToAccounts(addNewAccountDTO).GetAccount();
            if (allAccounts != null)
            {
                
                if (allAccounts.Contains(newAccount))
                {
                    throw new AccountNumberAlreadyExistsException($"Account Number {newAccount.AccountNumber} already exists");
                }
            }
            return await _accountsRepository.Add(newAccount);
        }

        public async Task<Accounts> DeleteAccount(int accountID)
        {
            var deletedAccount = await _accountsRepository.Delete(accountID);
            if(deletedAccount == null) 
            {
                throw new NoAccountsFoundException($"Account ID {accountID} not found");
            }
            return deletedAccount;
        }

        public async Task<Accounts> GetAccount(int accountID)
        {
            var foundedAccount = await _accountsRepository.Get(accountID);
            if (foundedAccount == null)
            {
                throw new NoAccountsFoundException($"Account ID {accountID} not found");
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

        public async Task<List<Accounts>> GetAllCustomerApprovedAccounts(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAccounts = await GetAllAccounts();
            var allCustomerAccounts = allAccounts.Where(account => account.CustomerID == customerID && account.Status == "Open Account Request Approved").ToList();
            if (allCustomerAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No Approved Accounts Available for Customer ID {customerID}");
            }
            return allCustomerAccounts;
        }

        public async Task<List<Accounts>> GetAllAccountsStatus(string status)
        {
            var allAccounts = await GetAllAccounts();
            var allPendingAccounts = allAccounts.Where(account => account.Status == status).ToList();
            if(allPendingAccounts.Count == 0)
            {
                throw new NoAccountsFoundException($"No {status} Accounts Available");
            }
            return allPendingAccounts;
        }

        public async Task<Accounts> UpdateAccountBalance(int accountID, double balance)
        {
            var foundedAccount = await GetAccount(accountID);
            foundedAccount.Balance = balance;
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }

        public async Task<Accounts> UpdateAccountStatus(int accountID, string status)
        {
            var foundedAccount = await GetAccount(accountID);
            foundedAccount.Status = status;
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }

        public async Task<Accounts> CloseAccount(int accountID)
        {
            var foundedAccount = await GetAccount(accountID);
            foundedAccount.Status = "Close Account Request Pending";
            var updatedAccount = await _accountsRepository.Update(foundedAccount);
            return updatedAccount;
        }
    }
}
