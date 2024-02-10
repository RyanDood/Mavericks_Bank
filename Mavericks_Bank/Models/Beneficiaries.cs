using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Beneficiaries: IEquatable<Beneficiaries>
    {
        [Key]
        public int BeneficiaryID { get; set; }
        public long AccountNumber { get; set; }
        public string Name { get; set; }
        public int BranchID { get; set; }
        [ForeignKey("BranchID")]
        public Branches? Branches { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customers? Customers { get; set; }

        public Beneficiaries()
        {

        }

        public Beneficiaries(int beneficiaryID, long accountNumber, string name, int branchID, int customerID)
        {
            BeneficiaryID = beneficiaryID;
            AccountNumber = accountNumber;
            Name = name;
            BranchID = branchID;
            CustomerID = customerID;
        }

        public bool Equals(Beneficiaries? other)
        {
            return AccountNumber == other.AccountNumber;
        }
    }
}
