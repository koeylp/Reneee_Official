using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Product;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService)
    {
        private readonly IProductService _productService = productService;
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto productRequest)
        {
            return new ObjectResult(await _productService.CreateProduct(productRequest)) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
