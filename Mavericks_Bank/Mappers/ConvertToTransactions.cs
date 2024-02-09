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

        public ConvertToTransactions(AddTransactionDepositDTO addTransactionDepositDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionDepositDTO.Amount;
            transaction.Description = addTransactionDepositDTO.Description;
            transaction.TransactionType = "Deposit";
            transaction.Status = "Pending";
            transaction.SourceAccountNumber = addTransactionDepositDTO.SourceAccountNumber;
            transaction.DestinationAccountNumber = null;
        }

        public ConvertToTransactions(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            transaction = new Transactions();
            transaction.Amount = addTransactionWithdrawalDTO.Amount;
            transaction.Description = addTransactionWithdrawalDTO.Description;
            transaction.TransactionType = "Withdrawal";
            transaction.Status = "Pending";
            transaction.SourceAccountNumber = addTransactionWithdrawalDTO.SourceAccountNumber;
            transaction.DestinationAccountNumber = null;
        }

        public Transactions GetTransaction()
        {
            return transaction;
        }
    }
}
