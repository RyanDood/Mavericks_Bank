﻿using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Mavericks_Bank.Repositories
{
    public class AdminRepository : IRepository<int, Admin>
    {
        private readonly MavericksBankContext _mavericksBankContext;
        private readonly ILogger<AdminRepository> _loggerAdminRepository;

        public AdminRepository(MavericksBankContext mavericksBankContext, ILogger<AdminRepository> loggerAdminRepository)
        {
            _mavericksBankContext = mavericksBankContext;
            _loggerAdminRepository = loggerAdminRepository;
        }

        public async Task<Admin> Add(Admin item)
        {
            _mavericksBankContext.Admin.Add(item);
            await _mavericksBankContext.SaveChangesAsync();
            _loggerAdminRepository.LogInformation($"Added New Admin : {item.AdminID}");
            return item;
        }

        public async Task<Admin?> Delete(int key)
        {
            var foundedAdmin = await Get(key);
            if (foundedAdmin == null)
            {
                return null;
            }
            else
            {
                _mavericksBankContext.Admin.Remove(foundedAdmin);
                await _mavericksBankContext.SaveChangesAsync();
                _loggerAdminRepository.LogInformation($"Deleted Admin : {foundedAdmin.AdminID}");
                return foundedAdmin;
            }
        }

        public async Task<Admin?> Get(int key)
        {
            var foundedAdmin = await _mavericksBankContext.Admin.FirstOrDefaultAsync(admin => admin.AdminID == key);
            if (foundedAdmin == null)
            {
                return null;
            }
            else
            {
                _loggerAdminRepository.LogInformation($"Founded Admin : {foundedAdmin.AdminID}");
                return foundedAdmin;
            }
        }

        public async Task<List<Admin>?> GetAll()
        {
            var allAdmins = await _mavericksBankContext.Admin.ToListAsync();
            if (allAdmins.Count == 0)
            {
                return null;
            }
            else
            {
                _loggerAdminRepository.LogInformation("All Admins Returned");
                return allAdmins;
            }
        }

        public async Task<Admin> Update(Admin item)
        {
            _mavericksBankContext.Entry<Admin>(item).State = EntityState.Modified;
            await _mavericksBankContext.SaveChangesAsync();
            _loggerAdminRepository.LogInformation($"Updated Admin : {item.AdminID}");
            return item;
        }
    }
}
