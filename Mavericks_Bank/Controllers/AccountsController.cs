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

        [Route("GetAllAccountsStatus")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllAccountsStatus(string status)
        {
            try
            {
                return await _accountsService.GetAllAccountsStatus(status);
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

        [Route("GetAllCustomerApprovedAccounts")]
        [HttpGet]
        public async Task<ActionResult<List<Accounts>>> GetAllCustomerApprovedAccounts(int customerID)
        {
            try
            {
                return await _accountsService.GetAllCustomerApprovedAccounts(customerID);
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
        public async Task<ActionResult<Accounts>> GetAccount(int accountID)
        {
            try
            {
                return await _accountsService.GetAccount(accountID);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddAccount")]
        [HttpPost]
        public async Task<ActionResult<Accounts>> AddAccount(AddNewAccountDTO addNewAccountDTO)
        {
            try
            {
                return await _accountsService.AddAccount(addNewAccountDTO);
            }
            catch (AccountNumberAlreadyExistsException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("UpdateAccountBalance")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> UpdateAccountBalance(int accountID, double balance)
        {
            try
            {
                return await _accountsService.UpdateAccountBalance(accountID, balance);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("UpdateAccountStatus")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> UpdateAccountStatus(int accountID, string status)
        {
            try
            {
                return await _accountsService.UpdateAccountStatus(accountID, status);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("CloseAccount")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> CloseAccount(int accountID)
        {
            try
            {
                return await _accountsService.CloseAccount(accountID);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteAccount")]
        [HttpDelete]
        public async Task<ActionResult<Accounts>> DeleteAccount(int accountID)
        {
            try
            {
                return await _accountsService.DeleteAccount(accountID);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerAccountsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
