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
        private readonly ILogger<TransactionsService> _loggerTransactionsService;

        public TransactionsService(IRepository<int, Transactions> transactionsRepository, ILogger<TransactionsService> loggerTransactionsService)
        {
            _transactionsRepository = transactionsRepository;
            _loggerTransactionsService = loggerTransactionsService;
        }

        public async Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            Transactions newTransaction = new ConvertToTransactions(addTransactionTransferDTO).GetTransaction();
            return await _transactionsRepository.Add(newTransaction);
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

        public async Task<List<Transactions>> GetAllTransactions()
        {
            var allTransactions = await _transactionsRepository.GetAll();
            if(allTransactions == null)
            {
                throw new NoTransactionsFoundException("No Available Transactions Data");
            }
            return allTransactions;
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

        public async Task<Transactions> UpdateTransactionStatus(UpdateTransactionStatusDTO updateTransactionStatusDTO)
        {
            var foundedTransaction = await GetTransaction(updateTransactionStatusDTO.TransactionID);
            foundedTransaction.Status = updateTransactionStatusDTO.Status;
            var updatedTransaction = await _transactionsRepository.Update(foundedTransaction);
            return updatedTransaction;
        }
    }
}
