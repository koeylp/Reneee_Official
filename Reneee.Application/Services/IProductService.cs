using Reneee.Application.DTOs.Product;

namespace Reneee.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(CreateProductDto productRequest);
    }
}
