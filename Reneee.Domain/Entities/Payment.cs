using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public string Method { get; set; }
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
