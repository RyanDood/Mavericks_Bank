using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class AppliedLoansService : IAppliedLoansAdminService
    {
        private readonly IRepository<int,AppliedLoans> _appliedLoansRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILoansAdminService _loansService;
        private readonly ILogger<AppliedLoansService> _loggerAppliedLoansService;

        public AppliedLoansService(IRepository<int, AppliedLoans> appliedLoansRepository, ICustomersAdminService customersService, ILoansAdminService loansService, ILogger<AppliedLoansService> loggerAppliedLoansService)
        {
            _appliedLoansRepository = appliedLoansRepository;
            _customersService = customersService;
            _loansService = loansService;
            _loggerAppliedLoansService = loggerAppliedLoansService;
        }

        public async Task<AppliedLoans> AddAppliedLoan(ApplyLoanDTO applyLoanDTO)
        {
            var foundedLoan = await _loansService.GetLoan(applyLoanDTO.LoanID);
            if(applyLoanDTO.Amount > foundedLoan.LoanAmount)
            {
                throw new LoanAmountExceedsException("Entered Amount Exceeds the Available Loan Amount");
            }
            var newAppliedLoan = new ConvertToAppliedLoans(applyLoanDTO).GetAppliedLoan();
            var allAppliedLoans = await _appliedLoansRepository.GetAll();
            if(allAppliedLoans != null)
            {
                if (allAppliedLoans.Contains(newAppliedLoan))
                {
                    throw new AppliedLoanAlreadyExistsException("You have already applied for this Loan");
                }
            }
            return await _appliedLoansRepository.Add(newAppliedLoan);
        }

        public async Task<AppliedLoans> DeleteAppliedLoan(int loanApplicationID)
        {
            var deletedAppliedLoan = await _appliedLoansRepository.Delete(loanApplicationID);
            if(deletedAppliedLoan == null)
            {
                throw new NoAppliedLoansFoundException($"Loan Application ID {loanApplicationID} not found");
            }
            return deletedAppliedLoan;
        }

        public async Task<List<AppliedLoans>> GetAllAppliedLoans()
        {
            var allAppliedLoans = await _appliedLoansRepository.GetAll();
            if(allAppliedLoans == null )
            {
                throw new NoAppliedLoansFoundException("No Applied Loans Data Found");
            }
            return allAppliedLoans;
        }

        public async Task<List<AppliedLoans>> GetAllCustomerAppliedLoans(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAppliedLoans = await GetAllAppliedLoans();
            var allCustomerAppliedLoans = allAppliedLoans.Where(appliedLoan => appliedLoan.CustomerID == customerID).ToList();
            if(allCustomerAppliedLoans.Count == 0)
            {
                throw new NoAppliedLoansFoundException($"No Applied Loans found for Customer ID {customerID}");
            }
            return allCustomerAppliedLoans;
        }

        public async Task<List<AppliedLoans>> GetAllCustomerAvailedLoans(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allAppliedLoans = await GetAllAppliedLoans();
            var allCustomerAppliedLoans = allAppliedLoans.Where(appliedLoan => appliedLoan.CustomerID == customerID && appliedLoan.Status == "Approved").ToList();
            if (allCustomerAppliedLoans.Count == 0)
            {
                throw new NoAppliedLoansFoundException($"No Availed Loans found for Customer ID {customerID}");
            }
            return allCustomerAppliedLoans;
        }

        public async Task<AppliedLoans> GetAppliedLoan(int loanApplicationID)
        {
            var foundAppliedLoan = await _appliedLoansRepository.Get(loanApplicationID);
            if(foundAppliedLoan == null)
            {
                throw new NoAppliedLoansFoundException($"Loan Application ID {loanApplicationID} not found");
            }
            return foundAppliedLoan;
        }

        public async Task<AppliedLoans> UpdateAppliedLoanStatus(int loanApplicationID, string status)
        {
            var foundAppliedLoan = await GetAppliedLoan(loanApplicationID);
            foundAppliedLoan.Status = status;
            var updatedAppliedLoan = await _appliedLoansRepository.Update(foundAppliedLoan);
            return updatedAppliedLoan;
        }
    }
}
