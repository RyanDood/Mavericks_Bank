﻿using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mavericks_Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<CustomersController> _loggerCustomersController;

        public CustomersController(ICustomersAdminService customersAdminService, ILogger<CustomersController> loggerCustomersController)
        {
            _customersService = customersAdminService;
            _loggerCustomersController = loggerCustomersController;
        }

        [Route("GetAllCustomers")]
        [HttpGet]
        public async Task<ActionResult<List<Customers>>> GetAllCustomers()
        {
            try
            {
                return await _customersService.GetAllCustomers();
            }
            catch (NoCustomersFoundException e)
            {
                _loggerCustomersController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("GetCustomer")]
        [HttpGet]
        public async Task<ActionResult<Customers>> GetCustomer(int customerID)
        {
            try
            {
                return await _customersService.GetCustomer(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerCustomersController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("UpdateCustomerDetails")]
        [HttpPut]
        public async Task<ActionResult<Customers>> UpdateCustomerDetails(UpdateCustomerDTO updateCustomerDTO)
        {
            try
            {
                return await _customersService.UpdateCustomerDetails(updateCustomerDTO);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerCustomersController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }

        [Route("DeleteCustomer")]
        [HttpDelete]
        public async Task<ActionResult<Customers>> DeleteCustomer(int customerID)
        {
            try
            {
                return await _customersService.DeleteCustomer(customerID);
            }
            catch (NoCustomersFoundException e)
            {
                _loggerCustomersController.LogInformation(e.Message);
                return NotFound(e.Message);
            }
        }
    }
}
