using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
