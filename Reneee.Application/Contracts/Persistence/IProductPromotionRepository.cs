using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IProductPromotionRepository : IGenericRepository<ProductPromotion>
    {
        Task<ProductPromotion> GetByProductAttributeIdAndStatus(int id, int status);
        Task<IReadOnlyList<ProductPromotion>> GetByPromotionId(int promotionId);
        Task<IReadOnlyList<Product>> GetProductByPromotionId(int promotionId);
        Task AddRange(IEnumerable<ProductPromotion> entities);
    }
}
