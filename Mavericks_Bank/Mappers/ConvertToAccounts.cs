using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using System.Security.Principal;

namespace Mavericks_Bank.Mappers
{
    public class ConvertToAccounts
    {
        Accounts account;

        public ConvertToAccounts(AddNewAccountDTO addNewAccountDTO)
        {
            account = new Accounts();
            account.Balance = addNewAccountDTO.Balance;
            account.AccountType = addNewAccountDTO.AccountType;
            account.Status = "Pending";
            account.IFSC = addNewAccountDTO.IFSC;
            account.CustomerID = addNewAccountDTO.CustomerID;
        }

        public Accounts GetAccount()
        {
            return account;
        }
    }
}
