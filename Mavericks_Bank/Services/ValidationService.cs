using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Mappers;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Validation = Mavericks_Bank.Models.Validation;

namespace Mavericks_Bank.Services
{
    public class ValidationService : IValidationAdminService
    {
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly IRepository<int, Customers> _customersRepository;
        private readonly IRepository<int, BankEmployees> _bankEmployeesRepository;
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly ILogger<ValidationService> _loggerValidationService;
        private readonly ITokenService _tokenService;

        public ValidationService(IRepository<string, Validation> validationRepository, IRepository<int, Customers> customersRepository, IRepository<int, BankEmployees> bankEmployeesRepository, IRepository<int, Admin> adminRepository, ILogger<ValidationService> loggerValidationService)
        {
            _validationRepository = validationRepository;
            _customersRepository = customersRepository;
            _bankEmployeesRepository = bankEmployeesRepository;
            _adminRepository = adminRepository;
            _loggerValidationService = loggerValidationService;
        }

        public async Task<List<Validation>> GetAllValidations()
        {
            var allValidation = await _validationRepository.GetAll();
            if(allValidation == null)
            {
                throw new NoValidationFoundException("No Validation Data Found");
            }
            return allValidation;
        }

        public async Task<LoginValidationDTO> Login(LoginValidationDTO loginValidationDTO)
        {
            var foundedValidation = await _validationRepository.Get(loginValidationDTO.Email);
            if (foundedValidation == null)
            {
                throw new NoValidationFoundException($"Email ID {loginValidationDTO.Email} not Found");
            }

            var convertedPassword = ConvertToEncryptedPassword(loginValidationDTO.Password,foundedValidation.Key);
            var passwordMatch = IsPasswordMatches(convertedPassword, foundedValidation.Password);
            if (passwordMatch)
            {
                loginValidationDTO.Password = "";
                loginValidationDTO.UserType = foundedValidation.UserType;
                loginValidationDTO.Token = ""; //await _tokenService.GenerateToken(loginValidationDTO);
                return loginValidationDTO;
            }
            else
            {
                throw new NoValidationFoundException($"Incorrect Password");
            }
        }

        private byte[] ConvertToEncryptedPassword(string password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            var convertedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return convertedPassword;
        }

        private bool IsPasswordMatches(byte[] convertedPassword, byte[] existingPassword)
        {
            for (int i = 0; i < existingPassword.Length; i++)
            {
                if (convertedPassword[i] != existingPassword[i])
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<LoginValidationDTO> RegisterAdmin(RegisterValidationAdminDTO registerValidationAdminDTO)
        {
            var foundedValidation = await _validationRepository.Get(registerValidationAdminDTO.Email);
            if (foundedValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationAdminDTO.Email} already exists");
            }

            Validation newValidation = new ConvertToValidation(registerValidationAdminDTO).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            Admin newAdmin = new ConvertToAdmin(registerValidationAdminDTO).GetAdmin();
            var addedAdmin = await _adminRepository.Add(newAdmin);

            _loggerValidationService.LogInformation($"Successfully Registered Admin {addedAdmin.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = "" };
            return loginValidationDTO;
        }

        public async Task<LoginValidationDTO> RegisterBankEmployees(RegisterValidationBankEmployees registerValidationBankEmployees)
        {
            var foundedValidation = await _validationRepository.Get(registerValidationBankEmployees.Email);
            if (foundedValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationBankEmployees.Email} already exists");
            }

            Validation newValidation = new ConvertToValidation(registerValidationBankEmployees).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            BankEmployees newBankEmployee = new ConvertToBankEmployees(registerValidationBankEmployees).GetBankEmployees();
            var addedEmployee = await _bankEmployeesRepository.Add(newBankEmployee);

            _loggerValidationService.LogInformation($"Successfully Registered Bank Employee {addedEmployee.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = ""};
            return loginValidationDTO;
        }

        public async Task<LoginValidationDTO> RegisterCustomers(RegisterValidationCustomersDTO registerValidationCustomersDTO)
        {
            var foundedValidation = await _validationRepository.Get(registerValidationCustomersDTO.Email);
            if (foundedValidation != null)
            {
                throw new ValidationAlreadyExistsException($"Email ID {registerValidationCustomersDTO.Email} already exists");
            }

            Customers newCustomer = new ConvertToCustomers(registerValidationCustomersDTO).GetCustomer();
            var allCustomers = await _customersRepository.GetAll();
            if(allCustomers != null)
            {
                if(allCustomers.Contains(newCustomer))
                {
                    throw new CustomerAlreadyExistsException($"You already have an existing account");
                }
            }

            Validation newValidation = new ConvertToValidation(registerValidationCustomersDTO).GetValidation();
            var addedValidation = await _validationRepository.Add(newValidation);

            var addedCustomer = await _customersRepository.Add(newCustomer);

            _loggerValidationService.LogInformation($"Successfully Registered Customer {addedCustomer.Name}");

            LoginValidationDTO loginValidationDTO = new LoginValidationDTO { Email = addedValidation.Email, Password = "", UserType = addedValidation.UserType, Token = ""};
            return loginValidationDTO;
        }

        public async Task<LoginValidationDTO> ForgotPassword(LoginValidationDTO loginValidationDTO)
        {
            var foundedValidation = await _validationRepository.Get(loginValidationDTO.Email);
            if (foundedValidation == null)
            {
                throw new NoValidationFoundException($"Email ID {loginValidationDTO.Email} not Found");
            }

            var convertedPassword = ConvertToEncryptedPassword(loginValidationDTO.Password, foundedValidation.Key);
            var passwordMatch = IsPasswordMatches(convertedPassword, foundedValidation.Password);
            if (passwordMatch)
            {
                throw new ValidationAlreadyExistsException($"Entered existing password, Enter a new Password");
            }
            
            await GenerateEncryptedPassword(loginValidationDTO.Password, foundedValidation);

            LoginValidationDTO loginValidation = new LoginValidationDTO { Email = foundedValidation.Email, Password = "", UserType = foundedValidation.UserType, Token = "" };
            return loginValidation;
        }

        private async Task GenerateEncryptedPassword(string password,Validation foundedValidation)
        {
            HMACSHA512 hMACSHA512 = new HMACSHA512();
            foundedValidation.Key = hMACSHA512.Key;
            foundedValidation.Password = hMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(password));
            await UpdateValidation(foundedValidation);
        }

        private async Task UpdateValidation(Validation foundedValidation)
        {
            var updatedValidation = await _validationRepository.Update(foundedValidation);
            _loggerValidationService.LogInformation($"Successfully Updated Customer Password {updatedValidation.Email}");
        }
    }
}
