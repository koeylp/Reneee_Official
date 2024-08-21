using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Address { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        public decimal Total { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }


    }
}
