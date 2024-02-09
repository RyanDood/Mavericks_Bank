namespace Mavericks_Bank.Models.DTOs
{
    public class AddTransactionDepositDTO
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public long SourceAccountNumber { get; set; }
    }
}
