using Reneee.Application.DTOs.OrderDetails;
using Reneee.Application.DTOs.Payment;
using Reneee.Application.DTOs.User;

namespace Reneee.Application.DTOs.Order
{
    public class OrderDto
    {
        public int? Id { get; set; }
        public string? Address { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
        public int? Status { get; set; }
        public UserDto? User { get; set; }
        public PaymentDto? Payment { get; set; }
        public ICollection<OrderDetailsDto>? OrderDetails { get; set; }
    }
}
