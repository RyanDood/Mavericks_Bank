using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Accounts:IEquatable<Accounts>
    {
        [Key]
        public long AccountNumber { get; set; }
        public double Balance { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public string IFSC { get; set; }
        [ForeignKey("IFSC")]
        public Branches? Branches { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public Accounts()
        {

        }
        public Accounts(long accountNumber, double balance, string accountType, string status, string iFSC, int customerID)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            AccountType = accountType;
            Status = status;
            IFSC = iFSC;
            CustomerID = customerID;
        }

        public bool Equals(Accounts? other)
        {
            return AccountNumber == this.AccountNumber;
        }
    }
}
