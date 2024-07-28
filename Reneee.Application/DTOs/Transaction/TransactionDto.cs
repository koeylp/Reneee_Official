using Reneee.Application.DTOs.Order;
using Reneee.Application.DTOs.User;

namespace Reneee.Application.DTOs.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }
        public UserDto User { get; set; }
        public OrderDto Order { get; set; }
    }
}
