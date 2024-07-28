using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class AttributeValue
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int Status { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
