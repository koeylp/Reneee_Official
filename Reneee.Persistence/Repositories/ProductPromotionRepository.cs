using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductPromotionRepository(ApplicationDbContext dbContext) : GenericRepository<ProductPromotion>(dbContext), IProductPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddRange(IEnumerable<ProductPromotion> entities)
        {
            await _dbContext.Set<ProductPromotion>().AddRangeAsync(entities);
        }

        public async Task<ProductPromotion> GetByProductAttributeIdAndStatus(int id, int status)
        {
            return await _dbContext.ProductPromotions
                   .FirstOrDefaultAsync(c => c.Id == id && c.Status == status);
        }

        public async Task<IReadOnlyList<ProductPromotion>> GetByPromotionId(int promotionId)
        {
            return await _dbContext.ProductPromotions
                        .Where(predicate => predicate.Id == promotionId)
                        .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductByPromotionId(int promotionId)
        {
            var query = from product in _dbContext.Products
                        join productAttribute in _dbContext.ProductAttributes on product.Id equals productAttribute.ProductID
                        join productPromotion in _dbContext.ProductPromotions on productAttribute.Id equals productPromotion.ProductAttributeId
                        where productPromotion.PromotionId == promotionId
                        select product;

            return await query
                .Include(p => p.ProductAttributes)
                .ThenInclude(pa => pa.AttributeValue)
                .ToListAsync();
        }
    }
}
