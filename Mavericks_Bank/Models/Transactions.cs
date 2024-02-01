using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Transactions:IEquatable<Transactions>
    {
        [Key]
        public int TransactionID { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public long SourceAccountNumber { get; set; }
        [ForeignKey("SourceAccountNumber")]
        public Accounts? Accounts { get; set; }
        public long DestinationAccountNumber { get; set; }
        [ForeignKey("DestinationAccountNumber")]
        public Beneficiaries? Beneficiaries { get; set; }

        public Transactions(int transactionID, double amount, DateTime transactionDate, string description, string transactionType, long sourceAccountNumber, long destinationAccountNumber)
        {
            TransactionID = transactionID;
            Amount = amount;
            TransactionDate = transactionDate;
            Description = description;
            TransactionType = transactionType;
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
        }

        public bool Equals(Transactions? other)
        {
            return TransactionID == other.TransactionID;
        }
    }
}
