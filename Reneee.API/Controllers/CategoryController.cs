using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Category;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto categoryRequest)
        {
            var category = await _categoryService.CreateCategory(categoryRequest);
            return Ok(category);
        }
    }
}
