using Mavericks_Bank.Context;
using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mavericks_Bank
{
    [Order(10)]
    public class AppliedLoansServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllAppliedLoansNotFoundExceptionTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.GetAllAppliedLoans());
        }

        [Test, Order(2)]
        public async Task AddAppliedLoansAmountExceedsExceptionTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
            {
                Amount = 1200000,
                Purpose = "Health Issues",
                LoanID = 2,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<LoanAmountExceedsException>(async () => await appliedLoansService.AddAppliedLoan(applyLoanDTO));
        }

        [Test, Order(3)]
        public async Task AddAppliedLoansNoSavingsAccountExceptionTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
            {
                Amount = 600000,
                Purpose = "Health Issues",
                LoanID = 2,
                CustomerID = 3,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await appliedLoansService.AddAppliedLoan(applyLoanDTO));
        }

        [Test, Order(4)]
        public async Task AddAppliedLoansSavingsAccountNotApprovedExceptionTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
            {
                Amount = 600000,
                Purpose = "Health Issues",
                LoanID = 2,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await appliedLoansService.AddAppliedLoan(applyLoanDTO));
        }

        [Test, Order(5)]
        public async Task AddAppliedLoansTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);


            await accountsService.UpdateAccountStatus(2, "Open Account Request Approved");

            ApplyLoanDTO applyLoanDTO = new ApplyLoanDTO
            {
                Amount = 600000,
                Purpose = "Health Issues",
                LoanID = 2,
                CustomerID = 2,
            };

            //action
            var addedAppliedLoan = await appliedLoansService.AddAppliedLoan(applyLoanDTO);

            //assert
            Assert.AreEqual(1, addedAppliedLoan.LoanApplicationID);
        }

        [Test, Order(6)]
        public async Task GetAllAppliedLoansTest()
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var allAppliedLoans = await appliedLoansService.GetAllAppliedLoans();

            //assert

            Assert.AreNotEqual(0, allAppliedLoans.Count);
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task GetAllCustomerAppliedLoansCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await appliedLoansService.GetAllCustomerAppliedLoans(customerID));
        }

        [Test, Order(8)]
        [TestCase(3)]
        public async Task GetAllCustomerAppliedLoansNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.GetAllCustomerAppliedLoans(customerID));
        }

        [Test, Order(9)]
        [TestCase(2)]
        public async Task GetAllCustomerAppliedLoansTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var allCustomerAppliedLoans = await appliedLoansService.GetAllCustomerAppliedLoans(customerID);

            //assert
            Assert.AreNotEqual(0, allCustomerAppliedLoans.Count);
        }

        [Test, Order(10)]
        [TestCase(1)]
        public async Task GetAllCustomerAvailedLoansCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await appliedLoansService.GetAllCustomerAvailedLoans(customerID));
        }

        [Test, Order(11)]
        [TestCase(2)]
        public async Task GetAllCustomerAvailedLoansNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.GetAllCustomerAvailedLoans(customerID));
        }

        [Test, Order(12)]
        [TestCase(2,"Approved")]
        public async Task UpdateAppliedLoanNotFoundExceptionTest(int loanID,string status)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.UpdateAppliedLoanStatus(loanID,status));
        }

        [Test, Order(13)]
        [TestCase(1, "Approved")]
        public async Task UpdateAppliedLoanStatusTest(int loanID, string status)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var updatedAppliedLoan = await appliedLoansService.UpdateAppliedLoanStatus(loanID, status);

            //assert
            Assert.AreEqual(status, updatedAppliedLoan.Status);
        }


        [Test, Order(14)]
        [TestCase(2)]
        public async Task GetAllCustomerAvailedLoansTest(int customerID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var allCustomerAvailedLoans = await appliedLoansService.GetAllCustomerAvailedLoans(customerID);

            //assert
            Assert.AreNotEqual(0, allCustomerAvailedLoans.Count);
        }

        [Test, Order(15)]
        [TestCase("Rejected")]
        public async Task GetAllAppliedLoansStatusNotFoundExceptionTest(string status)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.GetAllAppliedLoansStatus(status));
        }

        [Test, Order(16)]
        [TestCase("Approved")]
        public async Task GetAllAppliedLoansStatusTest(string status)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var allAppliedLoansStatus = await appliedLoansService.GetAllAppliedLoansStatus(status);

            //assert
            Assert.AreNotEqual(0, allAppliedLoansStatus.Count);
        }

        [Test, Order(17)]
        [TestCase(2)]
        public async Task GetAppliedLoanNotFoundExceptionTest(int loanApplicationID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.GetAppliedLoan(loanApplicationID));
        }

        [Test, Order(18)]
        [TestCase(1)]
        public async Task GetAppliedLoanTest(int loanApplicationID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var foundAppliedLoan = await appliedLoansService.GetAppliedLoan(loanApplicationID);

            //assert
            Assert.AreEqual(loanApplicationID, foundAppliedLoan.LoanApplicationID);
        }

        [Test, Order(19)]
        [TestCase(2)]
        public async Task DeleteAppliedLoanNotFoundExceptionTest(int loanApplicationID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAppliedLoansFoundException>(async () => await appliedLoansService.DeleteAppliedLoan(loanApplicationID));
        }

        [Test, Order(20)]
        [TestCase(1)]
        public async Task DeleteAppliedLoanTest(int loanApplicationID)
        {
            //arrange
            var mockAppliedLoansLogger = new Mock<ILogger<AppliedLoansRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockLoansRepositoryLogger = new Mock<ILogger<LoansRepository>>();
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockLoansServiceLogger = new Mock<ILogger<LoansService>>();
            var mockAppliedLoansServiceLogger = new Mock<ILogger<AppliedLoansService>>();

            IRepository<int, AppliedLoans> appliedLoansRepository = new AppliedLoansRepository(mavericksBankContext, mockAppliedLoansLogger.Object);
            IRepository<int, Loans> loansRepository = new LoansRepository(mavericksBankContext, mockLoansRepositoryLogger.Object);
            ILoansAdminService loansService = new LoansService(loansRepository, mockLoansServiceLogger.Object);
            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IAppliedLoansAdminService appliedLoansService = new AppliedLoansService(appliedLoansRepository, customersService, loansService, accountsService, mockAppliedLoansServiceLogger.Object);

            //action
            var deleteAppliedLoan = await appliedLoansService.DeleteAppliedLoan(loanApplicationID);

            //assert
            Assert.AreEqual(loanApplicationID, deleteAppliedLoan.LoanApplicationID);
        }
    }
}
