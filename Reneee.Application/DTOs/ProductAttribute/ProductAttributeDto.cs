using Reneee.Application.DTOs.AttributeValue;

namespace Reneee.Application.DTOs.ProductAttribute
{
    public class ProductAttributeDto
    {
        public int? Id { get; set; }
        public decimal? AttributePrice { get; set; }
        public decimal AttributeDiscountPrice { get; set; }
        public int? Stock { get; set; }
        public int? Status { get; set; }
        public AttributeValueDto? AttributeValue { get; set; }
    }
}
