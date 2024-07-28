using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class Attribute
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
