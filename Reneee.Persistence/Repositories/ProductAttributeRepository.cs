using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductAttributeRepository(ApplicationDbContext dbContext) : GenericRepository<ProductAttribute>(dbContext), IProductAttributeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
