using Reneee.Application.DTOs.ProductAttribute;

namespace Reneee.Application.DTOs.OrderDetails
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public ProductAttributeInfoDto ProductAttributeInfo { get; set; }
    }
}
