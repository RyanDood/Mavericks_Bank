﻿using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IBanksAdminService:IBanksUserService
    {
        public Task<Banks> AddBank(Banks item);
        public Task<Banks> UpdateBankName(UpdateBankNameDTO updateBankNameDTO);
        public Task<Banks> DeleteBank(int key);
    }
}
