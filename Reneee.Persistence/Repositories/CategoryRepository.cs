using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class CategoryRepository(ApplicationDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IReadOnlyList<Category>> GetActiveCategories()
        {
            return await _dbContext.Categories
                .Where(c => c.Status == 1)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAndStatus(int id, int status)
        {
            return await _dbContext.Categories
                   .FirstOrDefaultAsync(c => c.Id == id && c.Status == status);
        }

        public async Task<Category> UpdateCategoryStatus(Category category, int newStatus)
        {
            category.Status = newStatus;
            await _dbContext.SaveChangesAsync();
            return category;
        }
    }
}
