using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Thumbnail { get; set; }
        public int? CategoryId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        [Column(TypeName = "ntext")]
        public string? Ingredients { get; set; }
        [Column(TypeName = "ntext")]
        public string? Guideline { get; set; }
        [Column(TypeName = "ntext")]
        public string? AdditionalInfo { get; set; }
        public int Status { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
