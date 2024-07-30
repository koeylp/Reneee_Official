using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetActiveCategories();
        Task<Category> UpdateCategoryStatus(Category category, int newStatus);
    }
}
