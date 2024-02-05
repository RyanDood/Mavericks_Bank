using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IBranchesUserService
    {
        public Task<List<Branches>> GetAllBranches();
        public Task<Branches> GetBranch(string key);
    }
}
