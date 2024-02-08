namespace Mavericks_Bank.Models.DTOs
{
    public class AddNewAccountDTO
    {
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public string IFSC { get; set; }
        public int CustomerID { get; set; }
    }
}
