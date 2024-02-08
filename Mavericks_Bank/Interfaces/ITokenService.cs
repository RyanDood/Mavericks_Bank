using Mavericks_Bank.Models.DTOs;

namespace Mavericks_Bank.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(LoginValidationDTO loginValidationDTO);
    }
}
