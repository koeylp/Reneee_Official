using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IAttributeValueRepository : IGenericRepository<AttributeValue>
    {
        Task<AttributeValue> GetByName(string value);
    }
}
