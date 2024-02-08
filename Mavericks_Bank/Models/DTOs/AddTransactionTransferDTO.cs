namespace Mavericks_Bank.Models.DTOs
{
    public class AddTransactionTransferDTO
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public long SourceAccountNumber { get; set; }
        public long DestinationAccountNumber { get; set; }
    }
}
