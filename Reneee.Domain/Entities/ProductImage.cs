using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Url { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public virtual Product Product { get; set; }
    }
}
