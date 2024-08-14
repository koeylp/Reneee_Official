using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{

    public class OrderRepository(ApplicationDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IReadOnlyList<Order>> GetOrderByUserAndStatus(User user, int? status)
        {
            var query = _dbContext.Orders.AsQueryable();

            query = query.Where(c => c.User == user);

            if (status != 100)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            return await query.ToListAsync();
        }
    }
}
