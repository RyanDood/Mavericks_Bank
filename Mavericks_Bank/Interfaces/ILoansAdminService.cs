using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ILoansAdminService:ILoansUserService
    {
        public Task<Loans> AddLoan(UpdateLoanDetailsDTO updateLoanDTO);
        public Task<Loans> UpdateLoanDetails(UpdateLoanDetailsDTO updateLoanDTO);
        public Task<Loans> DeleteLoan(int loanID);
    }
}
