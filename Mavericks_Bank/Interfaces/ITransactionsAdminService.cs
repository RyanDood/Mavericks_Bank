﻿using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITransactionsAdminService:ITransactionsUserService
    {
        public Task<List<Transactions>> GetAllTransactions();
        public Task<List<Transactions>> GetAllAccountTransactions(long accountNumber);
        public Task<Transactions> GetTransaction(int transactionID);
        public Task<Transactions> DeleteTransaction(int transactionID);
        public Task<Transactions> UpdateTransactionStatus(int transactionID, string status);
    }
}
