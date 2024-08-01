using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reneee.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Thumbnail { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        public int Status { get; set; }
    }
}
