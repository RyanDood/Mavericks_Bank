using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mavericks_Bank.Models
{
    public class Branches: IEquatable<Branches>
    {
        [Key]
        public string IFSCNumber { get; set; }
        public string BranchName { get; set; }
        public int BankID { get; set; }
        [ForeignKey("BankID")]
        public Banks? Banks { get; set; }

        public Branches()
        {

        }

        public Branches(string iFSCNumber, string branchName, int bankID)
        {
            IFSCNumber = iFSCNumber;
            BranchName = branchName;
            BankID = bankID;
        }

        public bool Equals(Branches? other)
        {
            return BranchName == other.BranchName;
        }
    }
}
