using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Beneficiaries: IEquatable<Beneficiaries>
    {
        [Key]
        public long AccountNumber { get; set; }
        public string Name { get; set; }
        public int BankID { get; set; }
        [ForeignKey("BankID")]
        public Banks? Banks { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public Beneficiaries(long accountNumber, string name, int bankID, int customerID)
        {
            AccountNumber = accountNumber;
            Name = name;
            BankID = bankID;
            CustomerID = customerID;
        }

        public bool Equals(Beneficiaries? other)
        {
            return AccountNumber == other.AccountNumber;
        }
    }
}
