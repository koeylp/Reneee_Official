using Reneee.Application.DTOs.Attribute;

namespace Reneee.Application.DTOs.AttributeValue
{
    public class AttributeValueDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Status { get; set; }
        public AttributeDto Attribute { get; set; }
    }
}
