namespace Reneee.Application.DTOs.Promotion
{
    public class CreatePromotionDto
    {
        public string? Description { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int[]? ProductAttributeIds { get; set; }
    }
}
