using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
using Reneee.Application.DTOs.Product;
using Reneee.Application.DTOs.ProductAttribute;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpPost]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto productRequest)
        {
            return new ObjectResult(await _productService.CreateProduct(productRequest)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        //[Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById([FromRoute] int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPut("enable/{id}")]
        public async Task<ActionResult<ProductDto>> EnableProduct([FromRoute] int id)
        {
            return Ok(await _productService.EnableProduct(id));
        }

        [HttpPut("disable/{id}")]
        public async Task<ActionResult<ProductDto>> DisableProduct([FromRoute] int id)
        {
            return Ok(await _productService.DisableProduct(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<string>> DeleteProduct([FromRoute] int id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> FilterProduct([FromQuery] decimal? filter_v_price_gte,
                                                                                    decimal? filter_v_price_lte,
                                                                                    string? sort_by,
                                                                                    int? filter_v_availability,
                                                                                    string? categories)
        {
            return Ok(await _productService.FilterProduct(filter_v_price_gte, filter_v_price_lte, sort_by, filter_v_availability, categories));
        }

        [HttpGet("promotion/{id}")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetByPromotionId([FromRoute] int id)
        {
            return Ok(await _productService.GetByPromotionId(id));
        }

        [HttpGet("search")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> SearchProduct([FromQuery] string? search)
        {
            return Ok(await _productService.SearchProduct(search));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleConstants.ROLE_STAFF)]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromRoute] int id, [FromBody] CreateProductDto productRequest)
        {
            return Ok(await _productService.UpdateProduct(id, productRequest));
        }

        [HttpGet("attribute")]
        public async Task<ActionResult<IReadOnlyList<ProductAttributeInfoDto>>> GetProductAttributes()
        {
            return Ok(await _productService.GetProductAttributes());
        }
    }
}
