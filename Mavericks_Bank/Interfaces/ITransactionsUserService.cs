using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITransactionsUserService
    {
        public Task<Transactions> AddTransactionTransfer(AddTransactionTransferDTO addNewTransferTransactionDTO);
    }
}
