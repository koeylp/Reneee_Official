using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class PromotionRepository(ApplicationDbContext dbContext) : GenericRepository<Promotion>(dbContext), IPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
