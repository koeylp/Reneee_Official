using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class OrderDetailsRepository(ApplicationDbContext dbContext) : GenericRepository<OrderDetails>(dbContext), IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
