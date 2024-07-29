using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductAttributeRepository(ApplicationDbContext dbContext) : GenericRepository<ProductAttribute>(dbContext), IProductAttributeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
