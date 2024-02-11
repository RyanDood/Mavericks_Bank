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
    public class BranchesServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllBranchesNotFoundExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.GetAllBranches());
        }

        [Test, Order(2)]
        public async Task AddBranchTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch1 = new Branches
            {
                BranchID = 1,
                IFSCNumber = "MAV2320001",
                BranchName = "MAV-Chennai",
                BankID = 2,
            };

            Branches branch2 = new Branches
            {
                BranchID = 2,
                IFSCNumber = "MAV2320002",
                BranchName = "Mavericks-Pune",
                BankID = 2,
            };

            Branches branch3 = new Branches
            {
                BranchID = 3,
                IFSCNumber = "HDFC2320003",
                BranchName = "HDFC-Chennai",
                BankID = 3,
            };

            //action
            var addedBranch = await branchesService.AddBranch(branch1);
            await branchesService.AddBranch(branch2);
            await branchesService.AddBranch(branch3);

            //assert
            Assert.AreEqual(1, addedBranch.BranchID);
        }

        [Test, Order(3)]
        public async Task AddBranchIFSCAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch = new Branches
            {
                BranchID = 4,
                IFSCNumber = "MAV2320001",
                BranchName = "MAV-Chennai",
                BankID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.AddBranch(branch));
        }

        [Test, Order(4)]
        public async Task AddBranchNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            Branches branch = new Branches
            {
                BranchID = 4,
                IFSCNumber = "MAV2320004",
                BranchName = "MAV-Chennai",
                BankID = 2,
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.AddBranch(branch));
        }

        [Test, Order(5)]
        public async Task GetAllBranchesTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var allBranches = await branchesService.GetAllBranches();

            //assert
            Assert.AreNotEqual(0, allBranches.Count);
        }

        [Test, Order(6)]
        [TestCase(4)]
        public async Task GetBranchesNotFoundExceptionTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.GetBranch(branchID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task GetBranchesTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var foundBranch = await branchesService.GetBranch(branchID);

            //assert
            Assert.AreEqual(1, foundBranch.BranchID);
        }

        [Test, Order(8)]
        public async Task UpdateBranchNotFoundExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 4,
                BranchName = "MAV-Chennai"
            };

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.UpdateBranchName(updateBranchNameDTO));
        }

        [Test, Order(9)]
        public async Task UpdateBranchNameAlreadyExistsExceptionTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 1,
                BranchName = "MAV-Chennai"
            };

            //action and assert
            Assert.ThrowsAsync<BranchAlreadyExistsException>(async () => await branchesService.UpdateBranchName(updateBranchNameDTO));
        }

        [Test, Order(10)]
        public async Task UpdateBranchNameTest()
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            UpdateBranchNameDTO updateBranchNameDTO = new UpdateBranchNameDTO
            {
                branchID = 1,
                BranchName = "Mavericks-Chennai"
            };

            //action
            var updatedBranch = await branchesService.UpdateBranchName(updateBranchNameDTO);

            //assert
            Assert.AreEqual(1, updatedBranch.BranchID);
        }

        [Test, Order(11)]
        [TestCase(4)]
        public async Task DeleteBranchesNotFoundExceptionTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoBranchesFoundException>(async () => await branchesService.DeleteBranch(branchID));
        }

        [Test, Order(12)]
        [TestCase(1)]
        public async Task DeleteBranchesTest(int branchID)
        {
            //arrange
            var mockBranchesRepositoryLogger = new Mock<ILogger<BranchesRepository>>();
            var mockBranchesServiceLogger = new Mock<ILogger<BranchesService>>();
            IRepository<int, Branches> branchesRepository = new BranchesRepository(mavericksBankContext, mockBranchesRepositoryLogger.Object);
            IBranchesAdminService branchesService = new BranchesService(branchesRepository, mockBranchesServiceLogger.Object);

            //action
            var deletedBranch = await branchesService.DeleteBranch(branchID);

            //assert
            Assert.AreEqual(1, deletedBranch.BranchID);
        }
    }
}
