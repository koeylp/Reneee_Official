using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
using Reneee.Application.DTOs.Category;
using Reneee.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto categoryRequest)
        {
            return new ObjectResult(await _categoryService.CreateCategory(categoryRequest)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpGet("active")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAllActiveCategories()
        {
            return Ok(await _categoryService.GetAllActiveCategories());
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = RoleConstants.ROLE_STAFF)]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Enable Category",
            Description = "Enable a category by its ID."
        )]
        public async Task<ActionResult<CategoryDto>> EnableCategory([FromRoute] int id)
        {
            return Ok(await _categoryService.EnableCategory(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory([FromRoute] int id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }
    }
}
