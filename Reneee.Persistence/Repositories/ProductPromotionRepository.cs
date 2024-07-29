using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductPromotionRepository(ApplicationDbContext dbContext) : GenericRepository<ProductPromotion>(dbContext), IProductPromotionRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
    }
}
