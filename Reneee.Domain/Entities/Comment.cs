using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
