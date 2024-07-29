using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductPromotionRepository(ApplicationDbContext dbContext) : GenericRepository<ProductPromotion>(dbContext), IProductPromotionRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
    }
}
