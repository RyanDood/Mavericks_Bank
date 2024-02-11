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
    [Order(4)]
    public class AdminServiceTest
    {
        MavericksBankContext mavericksBankContext;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<MavericksBankContext>().UseInMemoryDatabase("MavericksBankDatabase").Options;
            mavericksBankContext = new MavericksBankContext(options);
        }

        [Test, Order(1)]
        public async Task GetAllAdminTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository,validationRepository,mockAdminServiceServiceLogger.Object);

            //action
            var allAdmins = await adminService.GetAllAdmins();

            //assert
            Assert.AreNotEqual(0, allAdmins.Count);
        }

        [Test, Order(2)]
        [TestCase(2)]
        public async Task GetAdminNotFoundExceptionTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.GetAdmin(adminID));
        }

        [Test, Order(3)]
        [TestCase(1)]
        public async Task GetAdminTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action 
            var foundedAdmin = await adminService.GetAdmin(adminID);

            //assert
            Assert.AreEqual(1, foundedAdmin.AdminID);
        }

        [Test, Order(4)]
        public async Task UpdateAdminNotFoundExceptionTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            UpdateAdminNameDTO updateAdminNameDTO = new UpdateAdminNameDTO
            {
                AdminID = 2,
                Name = "Black Rock"
            };

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.UpdateAdminName(updateAdminNameDTO));
        }

        [Test, Order(5)]
        public async Task UpdateAdminNameTest()
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            UpdateAdminNameDTO updateAdminNameDTO = new UpdateAdminNameDTO
            {
                AdminID = 1,
                Name = "Black Rock"
            };

            //action
            var updatedAdmin = await adminService.UpdateAdminName(updateAdminNameDTO);

            //assert
            Assert.AreEqual(1, updatedAdmin.AdminID);
        }

        [Test, Order(6)]
        [TestCase(2)]
        public async Task DeleteAdminNotFoundExceptionTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action and assert
            Assert.ThrowsAsync<NoAdminFoundException>(async () => await adminService.DeleteAdmin(adminID));
        }

        [Test, Order(7)]
        [TestCase(1)]
        public async Task DeleteAdminTest(int adminID)
        {
            //arrange
            var mockValidationRepositoryLogger = new Mock<ILogger<ValidationRepository>>();
            var mockAdminRepositoryLogger = new Mock<ILogger<AdminRepository>>();
            var mockAdminServiceServiceLogger = new Mock<ILogger<AdminService>>();

            IRepository<string, Validation> validationRepository = new ValidationRepository(mavericksBankContext, mockValidationRepositoryLogger.Object);
            IRepository<int, Admin> adminRepository = new AdminRepository(mavericksBankContext, mockAdminRepositoryLogger.Object);
            IAdminService adminService = new AdminService(adminRepository, validationRepository, mockAdminServiceServiceLogger.Object);

            //action 
            var foundedAdmin = await adminService.DeleteAdmin(adminID);

            //assert
            Assert.AreEqual(1, foundedAdmin.AdminID);
        }
    }
}
