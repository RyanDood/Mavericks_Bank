using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class AppliedLoansRepository : IRepository<int, AppliedLoans>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<AppliedLoansRepository> _loggerAppliedLoansRepository;

        public AppliedLoansRepository(MavericksBankContext mavericksBankContext, ILogger<AppliedLoansRepository> loggerAppliedLoansRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerAppliedLoansRepository = loggerAppliedLoansRepository;
        }

        public async Task<AppliedLoans> Add(AppliedLoans item)
        {
            _mavericksBankContext.AppliedLoans.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerAppliedLoansRepository.LogInformation($"Added New Applied Loan : {item.LoanApplicationID}");
            return item;
        }

        public async Task<AppliedLoans?> Delete(int key)
        {
            var foundedAppliedLoan = await Get(key);
            if (foundedAppliedLoan == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.AppliedLoans.Remove(foundedAppliedLoan);
                await _mavericksBankContext.SaveChangesAsync();
                return foundedAppliedLoan;
            }
        }

        public async Task<AppliedLoans?> Get(int key)
        {
            var foundedAppliedLoan = await _mavericksBankContext.AppliedLoans.FirstOrDefaultAsync(loan => loan.LoanApplicationID == key);
            if (foundedAppliedLoan == null)
            {
                return null;
            }
            else
            {
                return foundedAppliedLoan;
            }
        }

        public async Task<List<AppliedLoans>?> GetAll()
        {
            var allAppliedLoans = await _mavericksBankContext.AppliedLoans.ToListAsync();
            if (allAppliedLoans.Count == 0)
            {
                return null;
            }
            else
            {
                return allAppliedLoans;
            }
        }

        public async Task<AppliedLoans> Update(AppliedLoans item)
        {
            _mavericksBankContext.Entry<AppliedLoans>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            return item;
        }
    }
}
