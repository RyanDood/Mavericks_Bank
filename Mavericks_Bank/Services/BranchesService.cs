using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class BranchesService : IBranchesAdminService
    {
        private readonly IRepository<int, Branches> _branchesRepository;
        private readonly ILogger<BranchesService> _loggerBranchesService;

        public BranchesService(IRepository<int, Branches> branchesRepository, ILogger<BranchesService> loggerBranchesService)
        {
            _branchesRepository = branchesRepository;
            _loggerBranchesService = loggerBranchesService;
        }

        public async Task<Branches> AddBranch(Branches branch)
        {
            var allBranches = await _branchesRepository.GetAll();
            if(allBranches != null)
            {
                var foundedBranchIFSC = allBranches.FirstOrDefault(branches => branches.IFSCNumber == branch.IFSCNumber);
                if(foundedBranchIFSC != null)
                {
                    throw new BranchAlreadyExistsException($"Branch IFSC {branch.IFSCNumber} already exists");
                }
                if (allBranches.Contains(branch))
                {
                    throw new BranchAlreadyExistsException($"Branch Name {branch.BranchName} already exists");
                }
            }
            return await _branchesRepository.Add(branch);
        }

        public async Task<Branches> DeleteBranch(int branchID)
        {
            var deletedBranch = await _branchesRepository.Delete(branchID);
            if(deletedBranch == null) 
            {
                throw new NoBranchesFoundException($"Branch ID {branchID} not found");
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

        public async Task<Branches> GetBranch(int branchID)
        {
            var foundBranch = await _branchesRepository.Get(branchID);
            if(foundBranch == null)
            {
                throw new NoBranchesFoundException($"Branch ID {branchID} not found");
            }
            return foundBranch;
        }

        public async Task<Branches> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO)
        {
            var foundBranch = await GetBranch(updateBranchNameDTO.branchID);
            var allBranches = await _branchesRepository.GetAll();
            if (allBranches != null)
            {
                var foundedBranchName = allBranches.FirstOrDefault(branches => branches.BranchName == updateBranchNameDTO.BranchName);
                if (foundedBranchName != null)
                {
                    throw new BranchAlreadyExistsException($"Branch Name {updateBranchNameDTO.BranchName} already exists");
                }
            }
            foundBranch.BranchName = updateBranchNameDTO.BranchName;
            var updatedBranch = await _branchesRepository.Update(foundBranch);
            return updatedBranch;
        }
    }
}
