using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class LoansService : ILoansAdminService
    {
        private readonly IRepository<int,Loans> _loansRepository;
        private readonly ILogger<LoansService> _loggerLoanService;

        public LoansService(IRepository<int, Loans> loansRepository, ILogger<LoansService> loggerLoanService)
        {
            _loansRepository = loansRepository;
            _loggerLoanService = loggerLoanService;
        }

        public async Task<Loans> AddLoan(UpdateLoanDetailsDTO updateLoanDTO)
        {
            Loans newLoan = new ConvertToLoans(updateLoanDTO).GetLoan();
            return await _loansRepository.Add(newLoan);
        }

        public async Task<Loans> DeleteLoan(int loanID)
        {
            var deletedLoan = await _loansRepository.Delete(loanID);
            if (deletedLoan == null) 
            {
                throw new NoLoansFoundException($"Loan ID {loanID} not found");
            }
            return deletedLoan;
        }

        public async Task<List<Loans>> GetAllLoans()
        {
            var allLoans = await _loansRepository.GetAll();
            if(allLoans == null)
            {
                throw new NoLoansFoundException("No Available Loans Data");
            }
            return allLoans;
        }

        public async Task<Loans> GetLoan(int loanID)
        {
            var foundedLoan = await _loansRepository.Get(loanID);
            if(foundedLoan == null)
            {
                throw new NoLoansFoundException($"Loan ID {loanID} not found");
            }
            return foundedLoan;
        }

        public async Task<Loans> UpdateLoanDetails(UpdateLoanDetailsDTO updateLoanDTO)
        {
            var foundedLoan = await GetLoan(updateLoanDTO.LoanID);
            foundedLoan.LoanAmount = updateLoanDTO.LoanAmount;
            foundedLoan.LoanType = updateLoanDTO.LoanType;
            foundedLoan.Interest = updateLoanDTO.Interest;
            foundedLoan.Tenure = updateLoanDTO.Tenure;
            var updatedLoan = await _loansRepository.Update(foundedLoan);
            return updatedLoan;
        }
    }
}
