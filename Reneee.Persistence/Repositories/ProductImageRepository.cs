using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductImageRepository(ApplicationDbContext dbContext) : GenericRepository<ProductImage>(dbContext), IProductImageRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
