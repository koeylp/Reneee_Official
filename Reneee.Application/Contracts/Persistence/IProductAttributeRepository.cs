using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IProductAttributeRepository : IGenericRepository<ProductAttribute>
    {
        Task<IEnumerable<ProductAttribute>> GetByProductId(int productId);
        void RemoveRange(IEnumerable<ProductAttribute> entities);
    }
}
