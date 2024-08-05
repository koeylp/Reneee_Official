using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductImageRepository(ApplicationDbContext dbContext) : GenericRepository<ProductImage>(dbContext), IProductImageRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task AddRange(IEnumerable<ProductImage> entities)
        {
            await _dbContext.ProductImages.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<ProductImage>> GetByProductId(int productId)
        {
            return await _dbContext.ProductImages.Where(pi => pi.Product.Id == productId).ToListAsync();
        }

        public void RemoveRange(IEnumerable<ProductImage> entities)
        {
            _dbContext.ProductImages.RemoveRange(entities);
        }
    }
}
