namespace Reneee.Application.DTOs.OrderDetails
{
    public class CreateOrderDetailsDto
    {
        public int ProductAttributeId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
