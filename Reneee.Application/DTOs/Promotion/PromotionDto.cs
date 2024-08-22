using Reneee.Application.DTOs.ProductPromotion;

namespace Reneee.Application.DTOs.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        public ICollection<ProductPromotionDto>? ProductPromotions { get; set; }
    }
}
