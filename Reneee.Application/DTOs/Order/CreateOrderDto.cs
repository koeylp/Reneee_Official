using Reneee.Application.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace Reneee.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public string Address { get; set; }
        public decimal Total { get; set; }
        public int PaymentId { get; set; }
        public CreateOrderDetailsDto[] createOrderDetails { get; set; }
    }
}
