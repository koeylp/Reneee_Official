using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class PromotionRepository(ApplicationDbContext dbContext) : GenericRepository<Promotion>(dbContext), IPromotionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IReadOnlyList<Promotion>> GetActivePromotions()
        {
            return await _dbContext.Promotions
                        .Where(p => p.StartDate <= DateTime.UtcNow && p.Status == 1)
                        .ToListAsync();
        }
    }
}
