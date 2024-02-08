using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToLoans
    {
        Loans loan;

        public ConvertToLoans(UpdateLoanDetailsDTO updateLoanDetailsDTO)
        {
            loan = new Loans();
            loan.LoanAmount = updateLoanDetailsDTO.LoanAmount;
            loan.LoanType = updateLoanDetailsDTO.LoanType;
            loan.Interest = updateLoanDetailsDTO.Interest;
            loan.Tenure = updateLoanDetailsDTO.Tenure;
            loan.Purpose = "";
            loan.Status = "";
            loan.CustomerID = null;
        }

        public Loans GetLoan()
        {
            return loan;
        }
    }
}
