using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class OrderDetailsRepository(ApplicationDbContext dbContext) : GenericRepository<OrderDetails>(dbContext), IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddRange(IEnumerable<OrderDetails> entities)
        {
            await _dbContext.Set<OrderDetails>().AddRangeAsync(entities);
        }

        public async Task<IReadOnlyList<OrderDetails>> GetOrderDetailsByOrderId(int orderId)
        {
            return await _dbContext.OrderDetails
                            .Where(e => e.OrderId == orderId)
                            .ToListAsync();
        }
    }
}
