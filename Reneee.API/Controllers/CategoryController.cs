using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Category;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService; 
        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto categoryRequest)
        {
            var category = await _categoryService.CreateCategory(categoryRequest);
            return Ok(category);
        }
    }
}
