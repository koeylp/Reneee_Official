using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        [Required]
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
        public virtual ICollection<ProductPromotion>? ProductPromotions { get; set; }
    }
}
