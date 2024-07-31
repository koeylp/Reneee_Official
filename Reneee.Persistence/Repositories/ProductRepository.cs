using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Utils;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private const string DESCENDING = "SortDescending";

        public async Task<IReadOnlyList<Product>> GetFilteredProducts(decimal? filter_v_price_gte, decimal? filter_v_price_lte,
                                                                string? sort_by, int? filter_v_availability)
        {
            IQueryable<Product> query = _dbContext.Set<Product>();

            if (filter_v_price_gte.HasValue)
            {
                query = query.Where(p => p.DiscountPrice >= filter_v_price_gte);
            }

            if (filter_v_price_lte != null)
            {
                query = query.Where(p => p.DiscountPrice <= filter_v_price_lte);
            }

            if (filter_v_availability.HasValue)
            {
                query = query.Where(p => p.Status == filter_v_availability);
            }

            if (!string.IsNullOrEmpty(sort_by))
            {
                string type = StringUtils.GetSortDirection(sort_by);
                query = sort_by switch
                {
                    "name" => type == DESCENDING ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => type == DESCENDING ? query.OrderByDescending(p => p.DiscountPrice) : query.OrderBy(p => p.DiscountPrice),
                    "date" => (IQueryable<Product>)(type == DESCENDING ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)),
                    _ => query.OrderBy(p => p.Name),
                };
            }

            return await query.ToListAsync();
        }
    }
}
