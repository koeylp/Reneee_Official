using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductAttributeRepository(ApplicationDbContext dbContext) : GenericRepository<ProductAttribute>(dbContext), IProductAttributeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IEnumerable<ProductAttribute>> GetByProductId(int productId)
        {
            return await _dbContext.ProductAttributes.Where(pa => pa.Product.Id == productId).ToListAsync();
        }

        public void RemoveRange(IEnumerable<ProductAttribute> entities)
        {
            _dbContext.ProductAttributes.RemoveRange(entities);
        }
    }
}
