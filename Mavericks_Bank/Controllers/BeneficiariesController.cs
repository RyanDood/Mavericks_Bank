using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        private readonly IBeneficiariesAdminService _beneficiariesService;
        private readonly ILogger<BeneficiariesController> _loggerBeneficiariesController;

        public BeneficiariesController(IBeneficiariesAdminService beneficiariesService, ILogger<BeneficiariesController> loggerBeneficiariesController)
        {
            _beneficiariesService = beneficiariesService;
            _loggerBeneficiariesController = loggerBeneficiariesController;
        }

        [Route("GetAllBeneficiaries")]
        [HttpGet]
        public async Task<ActionResult<List<Beneficiaries>>> GetAllBeneficiaries()
        {
            try
            {
                return await _beneficiariesService.GetAllBeneficiaries();
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetAllCustomerBeneficiaries")]
        [HttpGet]
        public async Task<ActionResult<List<Beneficiaries>>> GetAllCustomerBeneficiaries(int customerID)
        {
            try
            {
                return await _beneficiariesService.GetAllCustomerBeneficiaries(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetBeneficiary")]
        [HttpGet]
        public async Task<ActionResult<Beneficiaries>> GetBeneficiary(long accountNumber)
        {
            try
            {
                return await _beneficiariesService.GetBeneficiary(accountNumber);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("AddBeneficiary")]
        [HttpPost]
        public async Task<ActionResult<Beneficiaries>> AddBeneficiary(Beneficiaries beneficiary)
        {
            try
            {
                return await _beneficiariesService.AddBeneficiary(beneficiary);
            }
            catch (BeneficiaryAlreadyExistsException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("DeleteBeneficiary")]
        [HttpDelete]
        public async Task<ActionResult<Beneficiaries>> DeleteBeneficiary(long accountNumber)
        {
            try
            {
                return await _beneficiariesService.DeleteBeneficiary(accountNumber);
            }
            catch (NoBeneficiariesFoundException e)
            {
                _loggerBeneficiariesController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
