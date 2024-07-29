using Reneee.Application.DTOs.Category;

namespace Reneee.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategory(CreateCategoryDto categoryRequest);
    }
}
