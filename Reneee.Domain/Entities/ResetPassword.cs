using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class ResetPassword
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Token { get; set; }
        public virtual User User { get; set; }
    }
}
