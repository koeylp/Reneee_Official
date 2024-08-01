using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetFilteredProducts(decimal? filter_v_price_gte, decimal? filter_v_price_lte,
                                                            string? sort_by, int? filter_v_availability);
        Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId);
    }
}
