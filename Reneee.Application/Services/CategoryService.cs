using Reneee.Application.DTOs.Category;

namespace Reneee.Application.Services
{
    public interface CategoryService
    {
        Task<CategoryDto> CreateCategory(CreateCategoryDto categoryRequest);
    }
}
