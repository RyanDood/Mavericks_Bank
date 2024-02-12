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
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllTransactionsNotFoundExceptionTest()
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
        public async Task AddTransactionDepositNoAccountsFoundExceptionTest()
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
        public async Task AddTransactionDepositInvalidAccountExceptionTest()
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
            Assert.AreEqual(1, addedDeposit.TransactionID);
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
            Assert.AreNotEqual(0, allAccounts.Count);
        }

        [Test, Order(6)]
        public async Task AddTransactionWithdrawalNoAccountsFoundExceptionTest()
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
        public async Task AddTransactionWithdrawalInvalidAccountExceptionTest()
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
        public async Task AddTransactionWithdrawalAmountExceedsBalanceExceptionTest()
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
            Assert.AreEqual(3, addedWithdrawal.TransactionID);
        }

        [Test, Order(10)]
        public async Task AddTransactionTransferNoAccountsFoundExceptionTest()
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
        public async Task AddTransactionTransferInvalidAccountExceptionTest()
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
        public async Task AddTransactionTransferNoBeneficiariesFoundExceptionTest()
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
        public async Task AddTransactionTransferAmountExceedsBalanceExceptionTest()
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
            Assert.AreEqual(5, addedTransfer.TransactionID);
        }

        [Test, Order(15)]
        public async Task AddTransactionTransferBeneficiaryNoAccountsFoundExceptionTest()
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
        public async Task AddTransactionTransferBeneficiaryInvalidAccountExceptionTest()
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
        public async Task AddTransactionTransferBeneficiaryAlreadyExistsExceptionTest()
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
        public async Task AddTransactionTransferBeneficiaryAmountExceedsBalanceExceptionTest()
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
            Assert.AreEqual(7, addedTransferBeneficiary.TransactionID);
        }

        [Test, Order(20)]
        [TestCase(4)]
        public async Task GetAllAccountTransactionsAccountNotFoundExceptionTest(int accountID)
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
        public async Task GetAllAccountTransactionsNotFoundExceptionTest(int accountID)
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
            Assert.AreNotEqual(0, allAccountTransactions.Count);
        }

        [Test, Order(23)]
        [TestCase(4)]
        public async Task GetAllCustomerTransactionsCustomerNotFoundExceptionTest(int customerID)
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
        public async Task GetAllCustomerTransactionsNotFoundExceptionTest(int customerID)
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
            Assert.AreNotEqual(0, allCustomerTransactions.Count);
        }

        [Test, Order(26)]
        [TestCase(4)]
        public async Task GetAccountInboundAndOutbooundTransactionsAccountNotFoundExceptionTest(int accountID)
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
        public async Task GetAccountInboundAndOutbooundTransactionsNotFoundExceptionTest(int accountID)
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
        public async Task GetCustomerInboundAndOutbooundTransactionsCustomerNotFoundExceptionTest(int customerID)
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
        public async Task GetCustomerInboundAndOutbooundTransactionsNotFoundExceptionTest(int customerID)
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
        public async Task GetLastTenAccountTransactionsAccountNotFoundExceptionTest(int accountID)
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
        public async Task GetLastTenAccountTransactionsNotFoundExceptionTest(int accountID)
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
            Assert.AreNotEqual(0, recentTenTransactions.Count);
        }

        [Test, Order(35)]
        [TestCase(4)]
        public async Task GetLastMonthAccountTransactionsAccountNotFoundExceptionTest(int accountID)
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
        public async Task GetLastMonthAccountTransactionsNotFoundExceptionTest(int accountID)
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
        public async Task GetTransactionsBetweenTwoDatesAccountNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
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
        public async Task GetTransactionsBetweenTwoDatesNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
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
        [TestCase(2, "2024-02-01", "2024-02-25")]
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
            Assert.AreNotEqual(0, transactionsBetweenDates.Count);
        }

        [Test, Order(40)]
        [TestCase(4, "2024-02-01", "2024-02-25")]
        public async Task GetAccountStatementAccountNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
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
        public async Task GetAccountStatementNotFoundExceptionTest(int accountID, DateTime fromDate, DateTime toDate)
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
        [TestCase(2, "2024-02-01", "2024-02-25")]
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
        public async Task GetTransactionNotFoundExceptionTest(int transactionID)
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
            Assert.AreEqual(1, foundedTransaction.TransactionID);
        }

        [Test, Order(45)]
        [TestCase(20,"Success")]
        public async Task UpdateTransactionNotFoundExceptionTest(int transactionID,string status)
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
            Assert.AreEqual(status, updatedTransaction.Status);
        }

        [Test, Order(47)]
        [TestCase(20)]
        public async Task DeleteTransactionNotFoundExceptionTest(int transactionID)
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
            Assert.AreEqual(1, deletedTransaction.TransactionID);
        }
    }
}
