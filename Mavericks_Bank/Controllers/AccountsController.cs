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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsAdminService _accountsService;
        private readonly ILogger<AccountsController> _loggerAccountsController;

        public AccountsController(IAccountsAdminService accountsService, ILogger<AccountsController> loggerAccountsController)
        {
            _accountsService = accountsService;
            _loggerAccountsController = loggerAccountsController;
        }

        [Route("GetAllAccounts")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccounts()
        {
            try
            {
                return await _accountsService.GetAllAccounts();
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAllCustomerAccounts")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllCustomerAccounts(int customerID)
        {
            try
            {
                return await _accountsService.GetAllCustomerAccounts(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAccount")]
        [HttpGet]
        public async Task<ActionResult<Accounts>> GetAccount(long accountNumber)
        {
            try
            {
                return await _accountsService.GetAccount(accountNumber);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddAccount")]
        [HttpPost]
        public async Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO)
        {
            return await _accountsService.AddAccount(addNewAccountDTO);
        }

        [Route("UpdateAccountBalance")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> UpdateAccountBalance(long accountNumber, double balance)
        {
            try
            {
                return await _accountsService.UpdateAccountBalance(accountNumber,balance);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("UpdateAccountStatus")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> UpdateAccountStatus(long accountNumber, string status)
        {
            try
            {
                return await _accountsService.UpdateAccountStatus(accountNumber,status);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteAccount")]
        [HttpDelete]
        public async Task<ActionResult<Accounts>> DeleteAccount(long accountNumber)
        {
            try
            {
                return await _accountsService.DeleteAccount(accountNumber);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
