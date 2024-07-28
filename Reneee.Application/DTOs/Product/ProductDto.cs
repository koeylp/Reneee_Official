using Reneee.Application.DTOs.Category;
using Reneee.Application.DTOs.ProductImage;

namespace Reneee.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string? Description { get; set; }
        public string? Ingredients { get; set; }
        public string? guideline { get; set; }
        public string? AdditionalInfo { get; set; }
        public int Status { get; set; }
        public CategoryDto Category { get; set; }
        public ICollection<ProductImageDto> ProductImages { get; set; }
    }
}
