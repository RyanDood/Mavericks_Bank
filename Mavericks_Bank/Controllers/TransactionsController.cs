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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsAdminService _transactionsService;
        private readonly ILogger<TransactionsController> _loggerTransactionsController;

        public TransactionsController(ITransactionsAdminService transactionsService, ILogger<TransactionsController> loggerTransactionsController)
        {
            _transactionsService = transactionsService;
            _loggerTransactionsController = loggerTransactionsController;
        }

        [Route("GetAllTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetAllTransactions()
        {
            try
            {
                return await _transactionsService.GetAllTransactions();
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAllCustomerTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetAllCustomerTransactions(int customerID)
        {
            try
            {
                return await _transactionsService.GetAllCustomerTransactions(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAllAccountTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetAllAccountTransactions(int accountID)
        {
            try
            {
                return await _transactionsService.GetAllAccountTransactions(accountID);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AccountFinancialPerformanceReport")]
        [HttpGet]
        public async Task<ActionResult<InboundAndOutboundTransactions>> GetAccountInboundAndOutbooundTransactions(int accountID)
        {
            try
            {
                return await _transactionsService.GetAccountInboundAndOutbooundTransactions(accountID);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("CustomerFinancialPerformanceReport")]
        [HttpGet]
        public async Task<ActionResult<InboundAndOutboundTransactions>> GetCustomerInboundAndOutbooundTransactions(int customerID)
        {
            try
            {
                return await _transactionsService.GetCustomerInboundAndOutbooundTransactions(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetTransaction")]
        [HttpGet]
        public async Task<ActionResult<Transactions>> GetTransaction(int transactionID)
        {
            try
            {
                return await _transactionsService.GetTransaction(transactionID);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetRecentTenAccountTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetLastTenAccountTransactions(int accountID)
        {
            try
            {
                return await _transactionsService.GetLastTenAccountTransactions(accountID);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetLastMonthAccountTransactions")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetLastMonthAccountTransactions(int accountID)
        {
            try
            {
                return await _transactionsService.GetLastMonthAccountTransactions(accountID);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetTransactionsBetweenTwoDates")]
        [HttpGet]
        public async Task<ActionResult<List<Transactions>>> GetTransactionsBetweenTwoDates(int accountID, DateTime fromDate, DateTime toDate)
        {
            try
            {
                return await _transactionsService.GetTransactionsBetweenTwoDates(accountID, fromDate,toDate);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("Deposit")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> AddTransactionDeposit(AddTransactionDepositDTO addTransactionDepositDTO)
        {
            try
            {
                return await _transactionsService.AddTransactionDeposit(addTransactionDepositDTO);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (TransactionAmountExceedsException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("Withdrawal")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> AddTransactionWithdrawal(AddTransactionWithdrawalDTO addTransactionWithdrawalDTO)
        {
            try
            {
                return await _transactionsService.AddTransactionWithdrawal(addTransactionWithdrawalDTO);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (TransactionAmountExceedsException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("Transfer")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            try
            {
                return await _transactionsService.AddTransactionTransfer(addTransactionTransferDTO);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (TransactionAmountExceedsException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("TransferWithBeneficiary")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> AddTransactionTransferBeneficiary(AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO)
        {
            try
            {
                return await _transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO);
            }
            catch (NoAccountsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (BeneficiaryAlreadyExistsException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
            catch (TransactionAmountExceedsException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("UpdateTransactionStatus")]
        [HttpPut]
        public async Task<ActionResult<Transactions>> UpdateTransactionStatus(int transactionID, string status)
        {
            try
            {
                return await _transactionsService.UpdateTransactionStatus(transactionID,status);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteTransaction")]
        [HttpDelete]
        public async Task<ActionResult<Transactions>> DeleteTransaction(int transactionID)
        {
            try
            {
                return await _transactionsService.DeleteTransaction(transactionID);
            }
            catch (NoTransactionsFoundException e)
            {
                _loggerTransactionsController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
