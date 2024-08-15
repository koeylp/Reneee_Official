using Reneee.Application.DTOs.Order;
using Reneee.Application.DTOs.User;

namespace Reneee.Application.DTOs.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string TransactionDate { get; set; }
        public string ClientSecret { get; set; }
        public int Status { get; set; }
        public UserDto User { get; set; }
        public OrderDto Order { get; set; }
    }
}
