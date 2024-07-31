namespace Reneee.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public string? Thumbnail { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? Description { get; set; }
        public string? Ingredients { get; set; }
        public string? Guideline { get; set; }
        public string? AdditionalInfo { get; set; }
        public int CategoryId { get; set; }
        public string?[] ProductImages { get; set; }
        
    }
}
