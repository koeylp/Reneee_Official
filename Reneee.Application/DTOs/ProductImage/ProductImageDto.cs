using Reneee.Application.DTOs.Product;

namespace Reneee.Application.DTOs.ProductImage
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public int Status { get; set; }
        public ProductDto? Product { get; set; }
    }
}
