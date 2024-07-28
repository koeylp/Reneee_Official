using Reneee.Application.DTOs.ProductAttribute;
using Reneee.Application.DTOs.Promotion;

namespace Reneee.Application.DTOs.ProductPromotion
{
    public class ProductPromotionDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public ProductAttributeDto productAttribute { get; set; }
        public PromotionDto Promotion { get; set; }
    }
}
