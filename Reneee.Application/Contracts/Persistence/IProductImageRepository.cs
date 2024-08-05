using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetByProductId(int productId);
        Task AddRange(IEnumerable<ProductImage> entities);
        void RemoveRange(IEnumerable<ProductImage> entities);
    }
}
