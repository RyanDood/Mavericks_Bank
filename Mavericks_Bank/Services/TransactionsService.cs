using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class TransactionsService : ITransactionsAdminService
    {
        private readonly IRepository<int, Transactions> _transactionsRepository; 

        private readonly ICustomersAdminService _customersService;
        private readonly IAccountsAdminService _accountservice;
        private readonly IBeneficiariesAdminService _beneficiaryService;
        private readonly ILogger<TransactionsService> _loggerTransactionsService;

        public TransactionsService(IRepository<int, Transactions> transactionsRepository, ICustomersAdminService customersService, IAccountsAdminService accountservice, IBeneficiariesAdminService beneficiaryService, ILogger<TransactionsService> loggerTransactionsService)
        {
            _transactionsRepository = transactionsRepository;
            _customersService = customersService;
            _accountservice = accountservice;
            _beneficiaryService = beneficiaryService;
            _loggerTransactionsService = loggerTransactionsService;
        }

        public async Task<Transactions> AddTransactionDeposit(AddTransactionDepositDTO addTransactionDepositDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionDepositDTO.SourceAccountNumber);

            Transactions newTransaction = new ConvertToTransactions(addTransactionDepositDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundedAccount.Balance + addTransactionDepositDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionDepositDTO.SourceAccountNumber, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionTransferDTO.SourceAccountNumber);
            await _beneficiaryService.GetBeneficiary(addTransactionTransferDTO.DestinationAccountNumber);

            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionTransferDTO.Amount > foundedAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }   

            await UpdateTransactionStatus(addedTransaction.TransactionID,"Success");
            var updatedBalance = foundedAccount.Balance - addTransactionTransferDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionTransferDTO.SourceAccountNumber, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionWithdrawalDTO.SourceAccountNumber);

            Transactions newTransaction = new ConvertToTransactions(addTransactionWithdrawalDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionWithdrawalDTO.Amount > foundedAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundedAccount.Balance - addTransactionWithdrawalDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionWithdrawalDTO.SourceAccountNumber, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> DeleteTransaction(int transactionID)
        {
            var deletedTransaction = await _transactionsRepository.Delete(transactionID);
            if(deletedTransaction == null)
            {
                throw new NoTransactionsFoundException($"Transaction ID {transactionID} not found");
            }
            return deletedTransaction;
        }

        public async Task<List<Transactions>> GetAllAccountTransactions(long accountNumber)
        {
            await _accountservice.GetAccount(accountNumber);
            var allTransactions = await GetAllTransactions();
            var allCustomerTransactions = allTransactions.Where(transaction => transaction.SourceAccountNumber == accountNumber).ToList();
            if (allCustomerTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account Number {accountNumber}");
            }
            return allCustomerTransactions;
        }

        public async Task<List<Transactions>> GetAllCustomerTransactions(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allCustomers = await _customersService.GetAllCustomers();
            var allAccounts = await _accountservice.GetAllAccounts();
            var allTransactions = await GetAllTransactions();   
            var allCustomerTransactions = (from customer in allCustomers
                                          join account in allAccounts on customer.CustomerID equals account.CustomerID
                                          join transaction in allTransactions on account.AccountNumber equals transaction.SourceAccountNumber
                                          where customer.CustomerID == customerID select transaction).ToList();
            if(allCustomerTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Customer ID {customerID}");
            }
            return allCustomerTransactions;
        }

        public async Task<List<Transactions>> GetAllTransactions()
        {
            var allTransactions = await _transactionsRepository.GetAll();
            if(allTransactions == null)
            {
                throw new NoTransactionsFoundException("No Available Transactions Data");
            }
            return allTransactions;
        }

        public async Task<List<Transactions>> GetLastMonthAccountTransactions(long accountNumber)
        {
            await _accountservice.GetAccount(accountNumber);
            var allTransactions = await GetAllTransactions();
            
            var lastMonthStartDate = DateTime.Today.AddMonths(-1).Date.AddDays(1 - DateTime.Today.Day);
            var lastMonthEndDate = DateTime.Today.AddDays(-DateTime.Today.Day);

            var allAccountTransactions = allTransactions.Where(transaction => transaction.SourceAccountNumber == accountNumber && transaction.TransactionDate >= lastMonthStartDate && transaction.TransactionDate <= lastMonthEndDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Last Month Transaction History Found for Account Number {accountNumber}");
            }
            return allAccountTransactions;
        }

        public async Task<List<Transactions>> GetLastTenAccountTransactions(long accountNumber)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountNumber);
            var allAccountTransactions = allTransactions.Where(transaction => transaction.SourceAccountNumber == accountNumber).OrderByDescending(transaction => transaction.TransactionID).Take(3).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account Number {accountNumber}");
            }
            return allAccountTransactions;
        }

        public async Task<Transactions> GetTransaction(int transactionID)
        {
            var foundedTransaction = await _transactionsRepository.Get(transactionID);
            if(foundedTransaction == null)
            {
                throw new NoTransactionsFoundException($"Transaction ID {transactionID} not found");
            }
            return foundedTransaction;
        }

        public async Task<List<Transactions>> GetTransactionsBetweenTwoDates(long accountNumber, DateTime fromDate, DateTime toDate)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountNumber);

            var allAccountTransactions = allTransactions.Where(transaction => transaction.SourceAccountNumber == accountNumber && transaction.TransactionDate >= fromDate && transaction.TransactionDate <= toDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account Number {accountNumber} within {fromDate} to {toDate}");
            }
            return allAccountTransactions;
        }

        public async Task<Transactions> UpdateTransactionStatus(int transactionID, string status)
        {
            var foundedTransaction = await GetTransaction(transactionID);
            foundedTransaction.Status = status;
            var updatedTransaction = await _transactionsRepository.Update(foundedTransaction);
            return updatedTransaction;
        }
    }
}
