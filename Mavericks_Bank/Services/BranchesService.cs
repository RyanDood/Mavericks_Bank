using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class BranchesService : IBranchesAdminService
    {
        private readonly IRepository<string, Branches> _branchesRepository;
        private readonly ILogger<BranchesService> _loggerBranchesService;

        public BranchesService(IRepository<string, Branches> branchesRepository, ILogger<BranchesService> loggerBranchesService)
        {
            _branchesRepository = branchesRepository;
            _loggerBranchesService = loggerBranchesService;
        }

        public async Task<Branches> AddBranch(Branches branch)
        {
            var foundedBranch = await _branchesRepository.Get(branch.IFSCNumber);
            if (foundedBranch != null)
            {
                throw new BranchAlreadyExistsException($"Branch IFSC {branch.IFSCNumber} already exists");
            }
            var allBranches = await _branchesRepository.GetAll();
            if (allBranches != null)
            {
                if (allBranches.Contains(branch))
                {
                    throw new BranchAlreadyExistsException($"Branch Name {branch.BranchName} already exists");
                }
            }
            return await _branchesRepository.Add(branch);
        }

        public async Task<Branches> DeleteBranch(string iFSC)
        {
            var deletedBranch = await _branchesRepository.Delete(iFSC);
            if(deletedBranch == null) 
            {
                throw new NoBranchesFoundException($"Branch IFSC {iFSC} not found");
            }
            return deletedBranch;
        }

        public async Task<List<Branches>> GetAllBranches()
        {
            var allBranches = await _branchesRepository.GetAll();
            if(allBranches == null)
            {
                throw new NoBranchesFoundException($"No Branches Data Found");
            }
            return allBranches;
        }

        public async Task<Branches> GetBranch(string iFSC)
        {
            var foundBranch = await _branchesRepository.Get(iFSC);
            if(foundBranch == null)
            {
                throw new NoBranchesFoundException($"Branch IFSC {iFSC} not found");
            }
            return foundBranch;
        }

        public async Task<Branches> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO)
        {
            var foundBranch = await GetBranch(updateBranchNameDTO.IFSCNumber);
            foundBranch.BranchName = updateBranchNameDTO.BranchName;
            var updatedBranch = await _branchesRepository.Update(foundBranch);
            return updatedBranch;
        }
    }
}
