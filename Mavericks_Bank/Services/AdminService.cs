using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<int, Admin> _adminRepository;
        private readonly IRepository<string, Validation> _validationRepository;
        private readonly ILogger<AdminService> _loggerAdminService;

        public AdminService(IRepository<int, Admin> adminRepository, IRepository<string, Validation> validationRepository, ILogger<AdminService> loggerAdminService)
        {
            _adminRepository = adminRepository;
            _validationRepository = validationRepository;
            _loggerAdminService = loggerAdminService;
        }

        public async Task<Admin> DeleteAdmin(int adminID)
        {
            var deletedAdmin = await _adminRepository.Delete(adminID);
            if(deletedAdmin == null)
            {
                throw new NoAdminFoundException($"Admin ID {adminID} not found");
            }
            await _validationRepository.Delete(deletedAdmin.Email);
            return deletedAdmin;
        }

        public async Task<Admin> GetAdmin(int adminID)
        {
            var foundedAdmin = await _adminRepository.Get(adminID);
            if(foundedAdmin == null)
            {
                throw new NoAdminFoundException($"Admin ID {adminID} not found");
            }
            return foundedAdmin;
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            var allAdmins = await _adminRepository.GetAll();
            if (allAdmins == null)
            {
                throw new NoAdminFoundException("No Available Admin Data");
            }
            return allAdmins;
        }

        public async Task<Admin> UpdateAdminName(UpdateAdminNameDTO updateAdminNameDTO)
        {
            var foundedAdmin = await GetAdmin(updateAdminNameDTO.AdminID);
            foundedAdmin.Name = updateAdminNameDTO.Name;
            var updatedAdmin = await _adminRepository.Update(foundedAdmin);
            return updatedAdmin;
        }
    }
}
