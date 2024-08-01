using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IProductPromotionRepository : IGenericRepository<ProductPromotion>
    {
        Task<ProductPromotion> GetByProductAttributeIdAndStatus(int id, int status);
    }
}
