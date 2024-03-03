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
    [Order(11)]
    public class TransactionsServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public void GetAllTransactionsNotFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetAllTransactions());
        }

        [Test, Order(2)]
        public void AddTransactionDepositNoAccountsFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionDepositDTO addTransactionDepositDTO = new AddTransactionDepositDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 4,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionDeposit(addTransactionDepositDTO));
        }

        [Test, Order(3)]
        public void AddTransactionDepositInvalidAccountExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionDepositDTO addTransactionDepositDTO = new AddTransactionDepositDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 3,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionDeposit(addTransactionDepositDTO));
        }

        [Test, Order(4)]
        public async Task AddTransactionDepositTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionDepositDTO addTransactionDepositDTO = new AddTransactionDepositDTO
            {
                Amount = 50,
                Description = "EMI",
                AccountID = 2,
            };

            //action
            var addedDeposit = await transactionsService.AddTransactionDeposit(addTransactionDepositDTO);

            //assert
            Assert.That(addedDeposit.TransactionID, Is.EqualTo(1));
        }

        [Test, Order(5)]
        public async Task GetAllTransactionsTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var allAccounts = await transactionsService.GetAllTransactions();

            //assert
            Assert.That(allAccounts.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(6)]
        public void AddTransactionWithdrawalNoAccountsFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionWithdrawalDTO addTransactionWithdrawalDTO = new AddTransactionWithdrawalDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 4
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionWithdrawal(addTransactionWithdrawalDTO));
        }

        [Test, Order(7)]
        public void AddTransactionWithdrawalInvalidAccountExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionWithdrawalDTO addTransactionWithdrawalDTO = new AddTransactionWithdrawalDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 3
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionWithdrawal(addTransactionWithdrawalDTO));
        }

        [Test, Order(8)]
        public void AddTransactionWithdrawalAmountExceedsBalanceExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionWithdrawalDTO addTransactionWithdrawalDTO = new AddTransactionWithdrawalDTO
            {
                Amount = 999999999,
                Description = "EMI",
                AccountID = 2
            };

            //action and assert
            Assert.ThrowsAsync<TransactionAmountExceedsException>(async () => await transactionsService.AddTransactionWithdrawal(addTransactionWithdrawalDTO));
        }

        [Test, Order(9)]
        public async Task AddTransactionWithdrawalTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionWithdrawalDTO addTransactionWithdrawalDTO = new AddTransactionWithdrawalDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 2
            };

            //action
            var addedWithdrawal = await transactionsService.AddTransactionWithdrawal(addTransactionWithdrawalDTO);

            //assert
            Assert.That(addedWithdrawal.TransactionID, Is.EqualTo(3));
        }

        [Test, Order(10)]
        public void AddTransactionTransferNoAccountsFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferDTO addTransactionTransferDTO = new AddTransactionTransferDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 4,
                BeneficiaryID = 2
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionTransfer(addTransactionTransferDTO));
        }

        [Test, Order(11)]
        public void AddTransactionTransferInvalidAccountExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferDTO addTransactionTransferDTO = new AddTransactionTransferDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 3,
                BeneficiaryID = 2
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionTransfer(addTransactionTransferDTO));
        }

        [Test, Order(12)]
        public void AddTransactionTransferNoBeneficiariesFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferDTO addTransactionTransferDTO = new AddTransactionTransferDTO
            {
                Amount = 5000,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryID = 4
            };

            //action and assert
            Assert.ThrowsAsync<NoBeneficiariesFoundException>(async () => await transactionsService.AddTransactionTransfer(addTransactionTransferDTO));
        }

        [Test, Order(13)]
        public void AddTransactionTransferAmountExceedsBalanceExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferDTO addTransactionTransferDTO = new AddTransactionTransferDTO
            {
                Amount = 999999999,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryID = 2
            };

            //action and assert
            Assert.ThrowsAsync<TransactionAmountExceedsException>(async () => await transactionsService.AddTransactionTransfer(addTransactionTransferDTO));
        }

        [Test, Order(14)]
        public async Task AddTransactionTransferTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferDTO addTransactionTransferDTO = new AddTransactionTransferDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryID = 2
            };

            //action
            var addedTransfer = await transactionsService.AddTransactionTransfer(addTransactionTransferDTO);

            //assert
            Assert.That(addedTransfer.TransactionID, Is.EqualTo(5));
        }

        [Test, Order(15)]
        public void AddTransactionTransferBeneficiaryNoAccountsFoundExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO = new AddTransactionTransferBeneficiaryDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 4,
                BeneficiaryAccountNumber = 1234567890,
                BeneficiaryName = "Cynthia",
                BranchID = 3,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO));
        }

        [Test, Order(16)]
        public void AddTransactionTransferBeneficiaryInvalidAccountExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO = new AddTransactionTransferBeneficiaryDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 3,
                BeneficiaryAccountNumber = 1234567890,
                BeneficiaryName = "Cynthia",
                BranchID = 3,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO));
        }

        [Test, Order(17)]
        public void AddTransactionTransferBeneficiaryAlreadyExistsExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO = new AddTransactionTransferBeneficiaryDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryAccountNumber = 1234987654,
                BeneficiaryName = "Elango",
                BranchID = 3,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BeneficiaryAlreadyExistsException>(async () => await transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO));
        }

        [Test, Order(18)]
        public void AddTransactionTransferBeneficiaryAmountExceedsBalanceExceptionTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO = new AddTransactionTransferBeneficiaryDTO
            {
                Amount = 999999999,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryAccountNumber = 7896232452,
                BeneficiaryName = "David",
                BranchID = 3,
                CustomerID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<TransactionAmountExceedsException>(async () => await transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO));
        }

        [Test, Order(19)]
        public async Task AddTransactionTransferBeneficiaryTest()
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            AddTransactionTransferBeneficiaryDTO addTransactionTransferBeneficiaryDTO = new AddTransactionTransferBeneficiaryDTO
            {
                Amount = 4000,
                Description = "EMI",
                AccountID = 2,
                BeneficiaryAccountNumber = 78962712268,
                BeneficiaryName = "Arun",
                BranchID = 3,
                CustomerID = 2,
            };

            //action
            var addedTransferBeneficiary = await transactionsService.AddTransactionTransferBeneficiary(addTransactionTransferBeneficiaryDTO);

            //assert
            Assert.That(addedTransferBeneficiary.TransactionID, Is.EqualTo(7));
        }

        [Test, Order(20)]
        [TestCase(4)]
        public void GetAllAccountTransactionsAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetAllAccountTransactions(accountID));
        }

        [Test, Order(21)]
        [TestCase(3)]
        public void GetAllAccountTransactionsNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetAllAccountTransactions(accountID));
        }

        [Test, Order(22)]
        [TestCase(2)]
        public async Task GetAllAccountTransactionsTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var allAccountTransactions = await transactionsService.GetAllAccountTransactions(accountID);

            //assert
            Assert.That(allAccountTransactions.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(23)]
        [TestCase(4)]
        public void GetAllCustomerTransactionsCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await transactionsService.GetAllCustomerTransactions(customerID));
        }

        [Test, Order(24)]
        [TestCase(3)]
        public void GetAllCustomerTransactionsNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetAllCustomerTransactions(customerID));
        }

        [Test, Order(25)]
        [TestCase(2)]
        public async Task GetAllCustomerTransactionsTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var allCustomerTransactions = await transactionsService.GetAllCustomerTransactions(customerID);

            //assert
            Assert.That(allCustomerTransactions.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(26)]
        [TestCase(4)]
        public void GetAccountInboundAndOutbooundTransactionsAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetAccountInboundAndOutbooundTransactions(accountID));
        }

        [Test, Order(27)]
        [TestCase(3)]
        public void GetAccountInboundAndOutbooundTransactionsNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetAccountInboundAndOutbooundTransactions(accountID));
        }

        [Test, Order(28)]
        [TestCase(2)]
        public async Task GetAccountInboundAndOutbooundTransactionsTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var allAccountTransactions = await transactionsService.GetAccountInboundAndOutbooundTransactions(accountID);

            //assert
            Assert.IsNotNull(allAccountTransactions);
        }

        [Test, Order(29)]
        [TestCase(4)]
        public void GetCustomerInboundAndOutbooundTransactionsCustomerNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoCustomersFoundException>(async () => await transactionsService.GetCustomerInboundAndOutbooundTransactions(customerID));
        }

        [Test, Order(30)]
        [TestCase(3)]
        public void GetCustomerInboundAndOutbooundTransactionsNotFoundExceptionTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetCustomerInboundAndOutbooundTransactions(customerID));
        }

        [Test, Order(31)]
        [TestCase(2)]
        public async Task GetCustomerInboundAndOutbooundTransactionsTest(int customerID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var allCustomerTransactions = await transactionsService.GetCustomerInboundAndOutbooundTransactions(customerID);

            //assert
            Assert.IsNotNull(allCustomerTransactions);
        }

        [Test, Order(32)]
        [TestCase(4)]
        public void GetLastTenAccountTransactionsAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetLastTenAccountTransactions(accountID));
        }

        [Test, Order(33)]
        [TestCase(3)]
        public void GetLastTenAccountTransactionsNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetLastTenAccountTransactions(accountID));
        }

        [Test, Order(34)]
        [TestCase(2)]
        public async Task GetLastTenAccountTransactionsTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var recentTenTransactions = await transactionsService.GetLastTenAccountTransactions(accountID);

            //assert
            Assert.That(recentTenTransactions.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(35)]
        [TestCase(4)]
        public void GetLastMonthAccountTransactionsAccountNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetLastMonthAccountTransactions(accountID));
        }

        [Test, Order(36)]
        [TestCase(3)]
        public void GetLastMonthAccountTransactionsNotFoundExceptionTest(int accountID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetLastMonthAccountTransactions(accountID));
        }

        [Test, Order(37)]
        [TestCase(4,"2024-02-01","2024-02-25")]
        public void GetTransactionsBetweenTwoDatesAccountNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetTransactionsBetweenTwoDates(accountID, fromDate, toDate));
        }

        [Test, Order(38)]
        [TestCase(3, "2024-02-01", "2024-02-25")]
        public void GetTransactionsBetweenTwoDatesNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetTransactionsBetweenTwoDates(accountID, fromDate, toDate));
        }

        [Test, Order(39)]
        [TestCase(2, "2024-02-01", "2024-03-25")]
        public async Task GetTransactionsBetweenTwoDatesTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var transactionsBetweenDates = await transactionsService.GetTransactionsBetweenTwoDates(accountID, fromDate, toDate);

            //assert
            Assert.That(transactionsBetweenDates.Count, Is.Not.EqualTo(0));
        }

        [Test, Order(40)]
        [TestCase(4, "2024-02-01", "2024-02-25")]
        public void GetAccountStatementAccountNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAccountsFoundException>(async () => await transactionsService.GetAccountStatement(accountID, fromDate, toDate));
        }

        [Test, Order(41)]
        [TestCase(3, "2024-02-01", "2024-02-25")]
        public void GetAccountStatementNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetAccountStatement(accountID, fromDate, toDate));
        }

        [Test, Order(42)]
        [TestCase(2, "2024-02-01", "2024-03-25")]
        public async Task GetAccountStatementTest(int accountID, DateTime fromDate, DateTime toDate)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var accountStatement = await transactionsService.GetAccountStatement(accountID, fromDate, toDate);

            //assert
            Assert.IsNotNull(accountStatement);
        }

        [Test, Order(43)]
        [TestCase(20)]
        public void GetTransactionNotFoundExceptionTest(int transactionID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.GetTransaction(transactionID));
        }

        [Test, Order(44)]
        [TestCase(1)]
        public async Task GetTransactionTest(int transactionID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var foundedTransaction = await transactionsService.GetTransaction(transactionID);

            //assert
            Assert.That(foundedTransaction.TransactionID, Is.EqualTo(1));
        }

        [Test, Order(45)]
        [TestCase(20,"Success")]
        public void UpdateTransactionNotFoundExceptionTest(int transactionID,string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.UpdateTransactionStatus(transactionID,status));
        }

        [Test, Order(46)]
        [TestCase(1, "Success")]
        public async Task UpdateTransactionStatusTransactionTest(int transactionID, string status)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var updatedTransaction = await transactionsService.UpdateTransactionStatus(transactionID, status);

            //assert
            Assert.That(updatedTransaction.Status, Is.EqualTo(status));
        }

        [Test, Order(47)]
        [TestCase(20)]
        public void DeleteTransactionNotFoundExceptionTest(int transactionID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoTransactionsFoundException>(async () => await transactionsService.DeleteTransaction(transactionID));
        }

        [Test, Order(48)]
        [TestCase(1)]
        public async Task DeleteTransactionTest(int transactionID)
        {
            //arrange
            var mockAccountsRepositoryLogger = new Mock<ILogger<AccountsRepository>>();
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockCustomerRepositoryLogger = new Mock<ILogger<CustomersRepository>>();
            var mockBeneficiariesRepositoryLogger = new Mock<ILogger<BeneficiariesRepository>>();
            var mockTransactionsRepositoryLogger = new Mock<ILogger<TransactionsRepository>>();
            var mockCustomerServiceLogger = new Mock<ILogger<CustomersService>>();
            var mockAccountsServiceLogger = new Mock<ILogger<AccountsService>>();
            var mockBeneficiariesServiceLogger = new Mock<ILogger<BeneficiariesService>>();
            var mockTransactionsServiceLogger = new Mock<ILogger<TransactionsService>>();

            IRepository<int, Accounts> accountsRepository = new AccountsRepository(mavericksBankContext, mockAccountsRepositoryLogger.Object);
            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Customers> customersRepository = new CustomersRepository(mavericksBankContext, mockCustomerRepositoryLogger.Object);
            IRepository<int, Beneficiaries> beneficiariesRepository = new BeneficiariesRepository(mavericksBankContext, mockBeneficiariesRepositoryLogger.Object);
            IRepository<int, Transactions> transactionsRepository = new TransactionsRepository(mavericksBankContext, mockTransactionsRepositoryLogger.Object);
            ICustomersAdminService customersService = new CustomersService(customersRepository, validationRepository, mockCustomerServiceLogger.Object);
            IAccountsAdminService accountsService = new AccountsService(accountsRepository, customersService, mockAccountsServiceLogger.Object);
            IBeneficiariesAdminService beneficiariesService = new BeneficiariesService(beneficiariesRepository, customersService, mockBeneficiariesServiceLogger.Object);
            ITransactionsAdminService transactionsService = new TransactionsService(transactionsRepository, customersService, accountsService, beneficiariesService, mockTransactionsServiceLogger.Object);

            //action
            var deletedTransaction = await transactionsService.DeleteTransaction(transactionID);

            //assert
            Assert.That(deletedTransaction.TransactionID, Is.EqualTo(1));
        }
    }
}
