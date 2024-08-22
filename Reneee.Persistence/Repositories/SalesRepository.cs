using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class SalesRepository(ApplicationDbContext dbContext) : GenericRepository<Sales>(dbContext), ISalesRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task DeleteSalesByProductAttribute(ProductAttribute productAttribute)
        {
            var salesToDelete = await _dbContext.Sales
               .Where(s => s.ProductAttribute == productAttribute)
               .ToListAsync();

            _dbContext.Sales.RemoveRange(salesToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalSales()
        {

            return await _dbContext.Sales.SumAsync(s => s.TotalSales);
        }
    }
}
