using Castle.Core.Configuration;
using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Test_Mavericks_Bank
{
    [Order(1)]
    public class ValidationServiceTest
    {
        MavericksBankContext mavericksBankContext;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllCustomersNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await customersService.GetAllCustomers());
        }

        [Test, Order(2)]
        public void GetAllBankEmployeesNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockBankEmployeesServiceLogger = new Mock<ILogger<BankEmployeesService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IBankEmployeesAdminService bankEmployeesService = new BankEmployeesService(bankEmployeesRepository, validationRepository, mockBankEmployeesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBankEmployeesFoundException>(async () => await bankEmployeesService.GetAllBankEmployees());
        }

        [Test, Order(3)]
        public void GetAllAdminNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.GetAllAdmins());
        }

        [Test,Order(4)]
        public async Task RegisterCustomersTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO 
            { 
                Email = "ryan@gmail.com",
                Password = "ryan123",
                Name = "Ryan",
                DOB = DateTime.Parse("2002-03-13"),
                Age = 21,
                PhoneNumber = 9876543210,
                Address = "Trichy,TN,India",
                AadharNumber = 775489653210,
                PANNumber = "WNJDN90892G",
                Gender = "Male"
            };

            RegisterValidationCustomersDTO registerValidationCustomersDTO2 = new RegisterValidationCustomersDTO
            {
                Email = "remon@gmail.com",
                Password = "remon123",
                Name = "Remon",
                DOB = DateTime.Parse("2002-03-13"),
                Age = 21,
                PhoneNumber = 8795626251,
                Address = "Trichy,TN,India",
                AadharNumber = 674516243210,
                PANNumber = "SFGW7850874S",
                Gender = "Male"
            };

            RegisterValidationCustomersDTO registerValidationCustomersDTO3 = new RegisterValidationCustomersDTO
            {
                Email = "jeff@gmail.com",
                Password = "jeff123",
                Name = "Jeffrey",
                DOB = DateTime.Parse("2001-11-10"),
                Age = 22,
                PhoneNumber = 4876265226,
                Address = "Pondy,TN,India",
                AadharNumber = 798565256267,
                PANNumber = "XOJWI54515E",
                Gender = "Male"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterCustomers(registerValidationCustomersDTO);
            await validationService.RegisterCustomers(registerValidationCustomersDTO2);
            await validationService.RegisterCustomers(registerValidationCustomersDTO3);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("ryan@gmail.com"));
        }

        [Test, Order(5)]
        public void RegisterCustomersEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO
            {
                Email = "ryan@gmail.com",
                Password = "ryan123",
                Name = "Ryan",
                DOB = DateTime.Parse("2002-03-13"),
                Age = 21,
                PhoneNumber = 9876543210,
                Address = "Trichy,TN,India",
                AadharNumber = 775489653210,
                PANNumber = "WNJDN90892G",
                Gender = "Male"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterCustomers(registerValidationCustomersDTO));
        }

        [Test, Order(6)]
        public void RegisterCustomersAccountExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationCustomersDTO registerValidationCustomersDTO = new RegisterValidationCustomersDTO
            {
                Email = "ryan2@gmail.com",
                Password = "ryan123",
                Name = "Ryan",
                DOB = DateTime.Parse("2002-03-13"),
                Age = 21,
                PhoneNumber = 9876543210,
                Address = "Trichy,TN,India",
                AadharNumber = 775489653210,
                PANNumber = "WNJDN90892G",
                Gender = "Male"
            };

            //action and assert
            Assert.ThrowsAsync<CustomerAlreadyExistsException>(async () => await validationService.RegisterCustomers(registerValidationCustomersDTO));
        }


        [Test, Order(7)]
        public async Task RegisterBankEmployeesTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationBankEmployees registerValidationBankEmployees = new RegisterValidationBankEmployees
            {
                Email = "tharun@maverick.in",
                Password = "tharun123",
                Name = "Tharun"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterBankEmployees(registerValidationBankEmployees);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("tharun@maverick.in"));
        }

        [Test, Order(8)]
        public void RegisterBankEmployeesEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationBankEmployees registerValidationBankEmployees = new RegisterValidationBankEmployees
            {
                Email = "tharun@maverick.in",
                Password = "tharun123",
                Name = "Tharun"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterBankEmployees(registerValidationBankEmployees));
        }

        [Test, Order(9)]
        public async Task RegisterAdminTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationAdminDTO registerValidationAdminDTO = new RegisterValidationAdminDTO
            {
                Email = "black@gmail.com",
                Password = "black123",
                Name = "Black"
            };

            //action
            LoginValidationDTO loginValidationDTO = await validationService.RegisterAdmin(registerValidationAdminDTO);

            //assert
            Assert.That(loginValidationDTO.Email, Is.EqualTo("black@gmail.com"));
        }

        [Test, Order(10)]
        public void RegisterAdminEmailExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            RegisterValidationAdminDTO registerValidationAdminDTO = new RegisterValidationAdminDTO
            {
                Email = "black@gmail.com",
                Password = "black123",
                Name = "Black"
            };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.RegisterAdmin(registerValidationAdminDTO));
        }

        [Test,Order(11)]
        public void LoginEmailNotExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan2@gmail.com", Password = "ryan123", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.Login(validationDTO));
        }

        [Test, Order(12)]
        public void LoginIncorrectPasswordExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan@gmail.com", Password = "ryan1234", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.Login(validationDTO));
        }

        [Test, Order(13)]
        public async Task LoginTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan@gmail.com", Password = "ryan123", UserType = "", Token = "" };

            //action
            var loginValidation = await validationService.Login(validationDTO);

            //assert
            Assert.That(loginValidation.Email, Is.EqualTo("ryan@gmail.com"));
        }

        [Test, Order(14)]
        public void ForgotPasswordEmailNotExistsExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan2@gmail.com", Password = "ryan123", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<NoValidationFoundException>(async () => await validationService.ForgotPassword(validationDTO));
        }

        [Test, Order(15)]
        public void ForgotPasswordExistsPasswordExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan@gmail.com", Password = "ryan123", UserType = "", Token = "" };

            //action and assert
            Assert.ThrowsAsync<ValidationAlreadyExistsException>(async () => await validationService.ForgotPassword(validationDTO));
        }

        [Test, Order(16)]
        public async Task ForgotPasswordTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockEmployeeRepositoryLogger = new Mock<ILogger<BankEmployeesRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockValidationServiceLogger = new Mock<ILogger<ValidationService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, BankEmployees> bankEmployeesRepository = new BankEmployeesRepository(mavericksBankContext, mockEmployeeRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IValidationAdminService validationService = new ValidationService(validationRepository, customersRepository, bankEmployeesRepository, adminRepository, mockValidationServiceLogger.Object);

            LoginValidationDTO validationDTO = new LoginValidationDTO { Email = "ryan@gmail.com", Password = "ryan1234", UserType = "", Token = "" };

            //action
            var loginValidation = await validationService.ForgotPassword(validationDTO);

            //assert
            Assert.That(loginValidation.Email, Is.EqualTo("ryan@gmail.com"));
        }
    }
}