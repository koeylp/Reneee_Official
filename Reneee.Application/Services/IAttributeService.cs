using Reneee.Application.DTOs.Attribute;
using Reneee.Application.DTOs.AttributeValue;

namespace Reneee.Application.Services
{
    public interface IAttributeService
    {
        Task<AttributeDto> CreateAttribute(CreateUpdateAttributeDto attributeRequest);
    }
}
