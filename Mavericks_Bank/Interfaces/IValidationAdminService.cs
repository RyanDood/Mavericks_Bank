using Mavericks_Bank.Models;

namespace Mavericks_Bank.Interfaces
{
    public interface IValidationAdminService:IValidationUserService
    {
        public Task<List<Validation>> GetAllValidations();
    }
}
