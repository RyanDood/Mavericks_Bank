using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mavericks_Bank.Repositories
{
    public class BanksRepository : IRepository<int, Banks>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<BanksRepository> _loggerBanksRepository;

        public BanksRepository(MavericksBankContext mavericksBankContext, ILogger<BanksRepository> loggerBanksRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerBanksRepository = loggerBanksRepository;
        }

        public async Task<Banks> Add(Banks item)
        {
            _mavericksBankContext.Banks.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBanksRepository.LogInformation($"Added New Bank : {item.BankName}");
            return item;
        }

        public async Task<Banks?> Delete(int key)
        {
            var foundedBank = await Get(key);
            if (foundedBank == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Banks.Remove(foundedBank);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerBanksRepository.LogInformation($"Removed Bank : {foundedBank.BankID}");
                return foundedBank;
            }
        }

        public async Task<Banks?> Get(int key)
        {
            var foundedBank = await _mavericksBankContext.Banks.FirstOrDefaultAsync(bank => bank.BankID == key);
            if (foundedBank == null)
            {
                return null;
            }
            else
            {
                _loggerBanksRepository.LogInformation($"Founded Bank : {foundedBank.BankID}");
                return foundedBank;
            }
        }

        public async Task<List<Banks>?> GetAll()
        {
            var allBanks = await _mavericksBankContext.Banks.ToListAsync();
            if (allBanks.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerBanksRepository.LogInformation($"Fetched All Banks Details");
                return allBanks;
            }
        }

        public async Task<Banks> Update(Banks item)
        {
            _mavericksBankContext.Entry<Banks>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerBanksRepository.LogInformation($"Updated Details for {item.BankID}");
            return item;
        }
    }
}
