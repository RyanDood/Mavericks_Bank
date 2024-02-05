using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesAdminService _branchesService;
        private readonly ILogger<BranchesController> _loggerBranchesController;

        public BranchesController(IBranchesAdminService branchesService, ILogger<BranchesController> loggerBranchesController)
        {
            _branchesService = branchesService;
            _loggerBranchesController = loggerBranchesController;
        }

        [Route("GetAllBranches")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetAllBranches()
        {
            try
            {
                return await _branchesService.GetAllBranches();
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetBranch")]
        [HttpGet]
        public async Task<ActionResult<Branches>> GetBranch(string key)
        {
            try
            {
                return await _branchesService.GetBranch(key);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddBranch")]
        [HttpPost]
        public async Task<ActionResult<Branches>> AddBranch(Branches item)
        {
            return await _branchesService.AddBranch(item);
        }

        [Route("UpdateBranchName")]
        [HttpPut]
        public async Task<ActionResult<Branches>> UpdateBranchName(UpdateBranchNameDTO updateBranchNameDTO)
        {
            try
            {
                return await _branchesService.UpdateBranchName(updateBranchNameDTO);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteBranch")]
        [HttpPut]
        public async Task<ActionResult<Branches>> DeleteBranch(string key)
        {
            try
            {
                return await _branchesService.DeleteBranch(key);
            }
            catch (NoBranchesFoundException e)
            {
                _loggerBranchesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
