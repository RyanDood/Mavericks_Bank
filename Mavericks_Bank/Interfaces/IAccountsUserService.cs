﻿using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface IAccountsUserService
    {
        public Task<List<Accounts>> GetAllCustomerAccounts(int customerID);
        public Task<Accounts> AddAccount(AddNewAccountDTO addNewAccountDTO);
        public Task<Accounts> DeleteAccount(long accountNumber);
    }
}