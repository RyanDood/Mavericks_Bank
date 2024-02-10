﻿using Mavericks_Bank.Context;
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
            var foundedAccount = await _accountservice.GetAccount(addTransactionDepositDTO.AccountID);

            Transactions newTransaction = new ConvertToTransactions(addTransactionDepositDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundedAccount.Balance + addTransactionDepositDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionDepositDTO.AccountID, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionTransferDTO.AccountID);
            await _beneficiaryService.GetBeneficiary(addTransactionTransferDTO.BeneficiaryID);

            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionTransferDTO.Amount > foundedAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }   

            await UpdateTransactionStatus(addedTransaction.TransactionID,"Success");
            var updatedBalance = foundedAccount.Balance - addTransactionTransferDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionTransferDTO.AccountID, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> AddTransactionTransferBeneficiary(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionTransferBeneficiaryDTO.AccountID);

            Beneficiaries newBeneficiary = new ConvertToBeneficiaries(addTransactionTransferBeneficiaryDTO).GetBeneficiary();
            var addedBeneficiary = await _beneficiaryService.AddBeneficiary(newBeneficiary);

            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferBeneficiaryDTO,addedBeneficiary.BeneficiaryID).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionTransferBeneficiaryDTO.Amount > foundedAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundedAccount.Balance - addTransactionTransferBeneficiaryDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionTransferBeneficiaryDTO.AccountID, updatedBalance);

            return addedTransaction;
        }

        public async Task<Transactions> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            var foundedAccount = await _accountservice.GetAccount(addTransactionWithdrawalDTO.AccountID);

            Transactions newTransaction = new ConvertToTransactions(addTransactionWithdrawalDTO).GetTransaction();
            var addedTransaction = await _transactionsRepository.Add(newTransaction);

            if (addTransactionWithdrawalDTO.Amount > foundedAccount.Balance)
            {
                await UpdateTransactionStatus(addedTransaction.TransactionID, "Failed");
                throw new TransactionAmountExceedsException("Amount entered exceeds the balance of your Account");
            }

            await UpdateTransactionStatus(addedTransaction.TransactionID, "Success");
            var updatedBalance = foundedAccount.Balance - addTransactionWithdrawalDTO.Amount;
            await _accountservice.UpdateAccountBalance(addTransactionWithdrawalDTO.AccountID, updatedBalance);

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

        public async Task<InboundAndOutboundTransactions> GetAccountInboundAndOutbooundTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.Status == "Success").ToList();
            if(allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No transactions found for {accountID}");
            }
            double allAccountDepositTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Deposit").ToList().Count();
            double allAccountTransferTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Transfer").ToList().Count();
            double allAccountWithdrawalTransactions = allAccountTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionType == "Withdrawal").ToList().Count();
            double ratio = allAccountDepositTransactions/(allAccountTransferTransactions + allAccountWithdrawalTransactions);
            var creditWorthy = ratio >= 1 ? "Yes" : "No";
            InboundAndOutboundTransactions inboundAndOutboundTransactions = new InboundAndOutboundTransactions{ TotalTransactions = allAccountTransactions.Count,InboundTransactions = allAccountDepositTransactions,OutboundTransactions = allAccountTransferTransactions + allAccountWithdrawalTransactions,Ratio = ratio,CreditWorthiness = creditWorthy };
            return inboundAndOutboundTransactions;
        }

        public async Task<List<Transactions>> GetAllAccountTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account ID {accountID}");
            }
            return allAccountTransactions;
        }

        public async Task<List<Transactions>> GetAllCustomerTransactions(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allCustomers = await _customersService.GetAllCustomers();
            var allAccounts = await _accountservice.GetAllAccounts();
            var allTransactions = await GetAllTransactions();   
            var allCustomerTransactions = (from customer in allCustomers
                                          join account in allAccounts on customer.CustomerID equals account.CustomerID
                                          join transaction in allTransactions on account.AccountID equals transaction.AccountID
                                          where customer.CustomerID == customerID select transaction).OrderByDescending(transaction => transaction.TransactionID).ToList();
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

        public async Task<InboundAndOutboundTransactions> GetCustomerInboundAndOutbooundTransactions(int customerID)
        {
            var allCustomerTransactions = await GetAllCustomerTransactions(customerID);
            var allSuccessfullCustomerTransactions = allCustomerTransactions.Where(transaction => transaction.Status == "Success").ToList();
            if (allSuccessfullCustomerTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No transactions found for {customerID}");
            }
            double allCustomerDepositTransactions = allSuccessfullCustomerTransactions.Where(transaction => transaction.TransactionType == "Deposit").ToList().Count();
            double allCustomerTransferTransactions = allSuccessfullCustomerTransactions.Where(transaction => transaction.TransactionType == "Transfer").ToList().Count();
            double allCustomerWithdrawalTransactions = allSuccessfullCustomerTransactions.Where(transaction => transaction.TransactionType == "Withdrawal").ToList().Count();
            double ratio = allCustomerDepositTransactions / (allCustomerTransferTransactions + allCustomerWithdrawalTransactions);
            var creditWorthy = ratio >= 1 ? "Yes" : "No";
            InboundAndOutboundTransactions inboundAndOutboundTransactions = new InboundAndOutboundTransactions { TotalTransactions = allSuccessfullCustomerTransactions.Count, InboundTransactions = allCustomerDepositTransactions, OutboundTransactions = allCustomerTransferTransactions + allCustomerWithdrawalTransactions, Ratio = ratio, CreditWorthiness = creditWorthy };
            return inboundAndOutboundTransactions;
        }

        public async Task<List<Transactions>> GetLastMonthAccountTransactions(int accountID)
        {
            await _accountservice.GetAccount(accountID);
            var allTransactions = await GetAllTransactions();
            
            var lastMonthStartDate = DateTime.Today.AddMonths(-1).Date.AddDays(1 - DateTime.Today.Day);
            var lastMonthEndDate = DateTime.Today.AddDays(-DateTime.Today.Day);

            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionDate >= lastMonthStartDate && transaction.TransactionDate <= lastMonthEndDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Last Month Transaction History Found for Account ID {accountID}");
            }
            return allAccountTransactions;
        }

        public async Task<List<Transactions>> GetLastTenAccountTransactions(int accountID)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountID);
            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID).OrderByDescending(transaction => transaction.TransactionID).Take(3).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account Number {accountID}");
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

        public async Task<List<Transactions>> GetTransactionsBetweenTwoDates(int accountID, DateTime fromDate, DateTime toDate)
        {
            var allTransactions = await GetAllTransactions();
            await _accountservice.GetAccount(accountID);

            var allAccountTransactions = allTransactions.Where(transaction => transaction.AccountID == accountID && transaction.TransactionDate >= fromDate && transaction.TransactionDate <= toDate).OrderByDescending(transaction => transaction.TransactionID).ToList();
            if (allAccountTransactions.Count == 0)
            {
                throw new NoTransactionsFoundException($"No Transaction History Found for Account ID {accountID} within {fromDate} to {toDate}");
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
