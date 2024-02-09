using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITransactionsUserService
    {
        public Task<List<Transactions>> GetAllCustomerTransactions(int customerID);
        public Task<List<Transactions>> GetLastTenAccountTransactions(long accountNumber);
        public Task<List<Transactions>> GetLastMonthAccountTransactions(long accountNumber);
        public Task<List<Transactions>> GetTransactionsBetweenTwoDates(long accountNumber,DateTime fromDate, DateTime toDate);
        public Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addNewTransferTransactionDTO);
        public Task<Transactions> AddTransactionDeposit(AddTransactionDepositDTO addTransactionDepositDTO);
        public Task<Transactions> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO);
    }
}
