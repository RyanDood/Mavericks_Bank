using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansAdminService _loanService;
        private readonly ILogger<LoansController> _loggerLoansController;

        public LoansController(ILoansAdminService loanService, ILogger<LoansController> loggerLoansController)
        {
            _loanService = loanService;
            _loggerLoansController = loggerLoansController;
        }

        [Route("GetAllLoans")]
        [HttpGet]
        public async Task<ActionResult<List<Loans>>> GetAllLoans()
        {
            try
            {
                return await _loanService.GetAllLoans();
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetLoan")]
        [HttpGet]
        public async Task<ActionResult<Loans>> GetLoan(int loanID)
        {
            try
            {
                return await _loanService.GetLoan(loanID);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddLoan")]
        [HttpPost]
        public async Task<Loans> AddLoan(UpdateLoanDetailsDTO updateLoanDTO)
        {
            return await _loanService.AddLoan(updateLoanDTO);
        }

        [Route("UpdateLoanDetails")]
        [HttpPut]
        public async Task<ActionResult<Loans>> UpdateLoanDetails(UpdateLoanDetailsDTO updateLoanDTO)
        {
            try
            {
                return await _loanService.UpdateLoanDetails(updateLoanDTO);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteLoan")]
        [HttpDelete]
        public async Task<ActionResult<Loans>> DeleteLoan(int loanID)
        {
            try
            {
                return await _loanService.DeleteLoan(loanID);
            }
            catch (NoLoansFoundException e)
            {
                _loggerLoansController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
