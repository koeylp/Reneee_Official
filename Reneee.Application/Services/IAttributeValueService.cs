using Reneee.Application.DTOs.AttributeValue;

namespace Reneee.Application.Services
{
    public interface IAttributeValueService
    {
        Task<AttributeValueDto> CreateAttributeValue(CreateAttributeValueDto attributeValueRequest);
    }
}
