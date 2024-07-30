using Reneee.Application.DTOs.Category;

namespace Reneee.Application.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> EnableCategory(int id);
        Task<CategoryDto> CreateCategory(CreateCategoryDto categoryRequest);
        Task<IReadOnlyList<CategoryDto>> GetAllActiveCategories();
        Task<IReadOnlyList<CategoryDto>> GetAllCategories();
    }
}
