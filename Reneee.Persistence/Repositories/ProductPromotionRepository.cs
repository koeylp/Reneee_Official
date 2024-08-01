using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductPromotionRepository(ApplicationDbContext dbContext) : GenericRepository<ProductPromotion>(dbContext), IProductPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<ProductPromotion> GetByProductAttributeIdAndStatus(int id, int status)
        {
            return await _dbContext.ProductPromotions
                   .FirstOrDefaultAsync(c => c.Id == id && c.Status == status);
        }
    }
}
