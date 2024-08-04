using Reneee.Application.DTOs.Attribute;

namespace Reneee.Application.Services
{
    public interface IAttributeService
    {
        Task<AttributeDto> CreateAttribute(CreateUpdateAttributeDto attributeRequest);
        Task<string> DeleteAttribute(int id);
        Task<IReadOnlyList<AttributeDto>> GetAllAttributes();
    }
}
