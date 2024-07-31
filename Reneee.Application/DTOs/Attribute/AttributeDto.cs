using Reneee.Application.DTOs.AttributeValue;

namespace Reneee.Application.DTOs.Attribute
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public ICollection<AttributeValueDto>? Values { get; set; }

    }
}
