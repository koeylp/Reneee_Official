using Reneee.Application.DTOs.Category;

namespace Reneee.Application.DTOs.Product
{
    public class ProductInfoDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Thumbnail { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? Ingredients { get; set; }
        public string? Guideline { get; set; }
        public string? AdditionalInfo { get; set; }
        public int Status { get; set; }
        public string? CreatedAt { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
