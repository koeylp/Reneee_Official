﻿using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Utils;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private const string DESCENDING = "desc";

        public async Task<IReadOnlyList<Product>> GetFilteredProducts(decimal? filter_v_price_gte, decimal? filter_v_price_lte,
                                                              string? sort_by, int? filter_v_availability, string? categories)
        {
            IQueryable<Product> query = _dbContext.Set<Product>();

            if (filter_v_price_gte.HasValue)
            {
                query = query.Where(p => p.DiscountPrice >= filter_v_price_gte);
            }

            if (filter_v_price_lte.HasValue)
            {
                query = query.Where(p => p.DiscountPrice <= filter_v_price_lte);
            }

            if (filter_v_availability.HasValue)
            {
                query = query.Where(p => p.Status == filter_v_availability);
            }

            if (!string.IsNullOrEmpty(categories))
            {
                var categoryList = categories.Split(',').Select(c => c.Trim()).ToList();
                query = query.Where(p => categoryList.Contains(p.Category.Name));
            }

            if (!string.IsNullOrEmpty(sort_by))
            {
                var type = StringUtils.GetSortDirection(sort_by);
                var sortType = StringUtils.GetSortType(sort_by);
                query = sortType switch
                {
                    "name" => type == DESCENDING ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => type == DESCENDING ? query.OrderByDescending(p => p.DiscountPrice) : query.OrderBy(p => p.DiscountPrice),
                    "date" => type == DESCENDING ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt),
                    _ => query.OrderBy(p => p.Name),
                };
            }

            return await query.ToListAsync();
        }


        public async Task<IReadOnlyList<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> Search(string search)
        {
            IQueryable<Product> query = _dbContext.Set<Product>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }
            return await query.ToListAsync();
        }
    }
}
