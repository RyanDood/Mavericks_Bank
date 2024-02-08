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

        [Route("AddTransactionTransfer")]
        [HttpPost]
        public async Task<ActionResult<Transactions>> AddTransactionTransfer(AddTransactionTransferDTO addTransactionTransferDTO)
        {
            return await _transactionsService.AddTransactionTransfer(addTransactionTransferDTO);
        }

        [Route("UpdateTransactionStatus")]
        [HttpPut]
        public async Task<ActionResult<Transactions>> UpdateTransactionStatus(UpdateTransactionStatusDTO updateTransactionStatusDTO)
        {
            try
            {
                return await _transactionsService.UpdateTransactionStatus(updateTransactionStatusDTO);
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
