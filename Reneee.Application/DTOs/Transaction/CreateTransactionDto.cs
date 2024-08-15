namespace Reneee.Application.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public string ClientSecret { get; set; }
        public int status { get; set; }
    }
}
