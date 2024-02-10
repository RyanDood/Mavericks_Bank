namespace Mavericks_Bank.Exceptions
{
    public class AccountNumberAlreadyExistsException : Exception
    {
        public AccountNumberAlreadyExistsException(string? message) : base(message)
        {

        }
    }
}
