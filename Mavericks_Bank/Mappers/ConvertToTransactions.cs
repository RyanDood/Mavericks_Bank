using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToTransactions
    {
        Transactions transaction;

        public ConvertToTransactions(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionTransferDTO.Amount;
            transaction.Description = addTransactionTransferDTO.Description;
            transaction.TransactionType = "Transfer";
            transaction.Status = "Pending";
            transaction.SourceAccountNumber = addTransactionTransferDTO.SourceAccountNumber;
            transaction.DestinationAccountNumber = addTransactionTransferDTO.DestinationAccountNumber;
        }

        public Transactions GetTransaction()
        {
            return transaction;
        }
    }
}
