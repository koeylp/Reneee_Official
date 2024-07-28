namespace Reneee.Application.DTOs.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
