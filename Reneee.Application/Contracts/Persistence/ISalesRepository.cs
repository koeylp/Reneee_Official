using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface ISalesRepository : IGenericRepository<Sales>
    {
        Task<decimal> GetTotalSales();
        Task DeleteSalesByProductAttribute(ProductAttribute productAttribute);
    }
}
