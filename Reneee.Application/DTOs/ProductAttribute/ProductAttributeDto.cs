using Reneee.Application.DTOs.AttributeValue;
using Reneee.Application.DTOs.Product;

namespace Reneee.Application.DTOs.ProductAttribute
{
    public class ProductAttributeDto
    {
        public int? Id { get; set; }
        public decimal? AttributePrice { get; set; }
        public int? Stock { get; set; }
        public int? Status { get; set; }
        public ProductDto? Product { get; set; }
        public AttributeValueDto? AttributeValue { get; set; }
    }
}
