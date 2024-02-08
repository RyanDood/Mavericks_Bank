using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class BankEmployeesService : IBankEmployeesAdminService
    {
        private readonly IRepository<int, BankEmployees> _bankEmployeesRepository;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ILogger<BankEmployeesService> _loggerBankEmployeesService;

        public BankEmployeesService(IRepository<int, BankEmployees> bankEmployeesRepository, IRepository<string, Validation> validationRepository, ILogger<BankEmployeesService> loggerBankEmployeesService)
        {
            _bankEmployeesRepository = bankEmployeesRepository;
            _validationRepository = validationRepository;
            _loggerBankEmployeesService = loggerBankEmployeesService;
        }

        public async Task<BankEmployees> DeleteBankEmployee(int employeeID)
        {
            var deletedBankEmployee = await _bankEmployeesRepository.Delete(employeeID);
            if(deletedBankEmployee == null)
            {
                throw new NoBankEmployeesFoundException($"Employee ID {employeeID} not found");
            }
            var deletedValidation = await _validationRepository.Delete(deletedBankEmployee.Email);
            if (deletedValidation == null)
            {
                throw new NoValidationFoundException($"Employee Validation {deletedBankEmployee.Email} not found");
            }
            _loggerBankEmployeesService.LogInformation($"Successfully Deleted the Customer : {deletedBankEmployee.EmployeeID}");
            return deletedBankEmployee;
        }

        public async Task<List<BankEmployees>> GetAllBankEmployees()
        {
            var allBankEmployees = await _bankEmployeesRepository.GetAll();
            if(allBankEmployees == null)
            {
                throw new NoBankEmployeesFoundException("No Available Bank Employees Data");
            }
            return allBankEmployees;
        }

        public async Task<BankEmployees> GetBankEmployee(int employeeID)
        {
            var foundedBankEmployee = await _bankEmployeesRepository.Get(employeeID);
            if(foundedBankEmployee == null)
            {
                throw new NoBankEmployeesFoundException($"Employee ID {employeeID} not found");
            }
            return foundedBankEmployee;
        }

        public async Task<BankEmployees> UpdateBankEmployeeName(UpdateBankEmployeeNameDTO updateBankEmployeeNameDTO)
        {
            var foundedBankEmployee = await GetBankEmployee(updateBankEmployeeNameDTO.EmployeeID);
            foundedBankEmployee.Name = updateBankEmployeeNameDTO.Name;
            var updatedBankEmployee = await _bankEmployeesRepository.Update(foundedBankEmployee);
            return updatedBankEmployee;
        }
    }
}
