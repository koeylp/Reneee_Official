using Reneee.Application.DTOs.Product;
using Reneee.Application.DTOs.ProductAttribute;

namespace Reneee.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(CreateProductDto productRequest);
        Task<string> DeleteProduct(int id);
        Task<ProductDto> DisableProduct(int id);
        Task<ProductDto> EnableProduct(int id);
        Task<IReadOnlyList<ProductDto>> FilterProduct(decimal? filter_v_price_gte, decimal? filter_v_price_lte, string? sort_by, int? filter_v_availability, string? categories);
        Task<IReadOnlyList<ProductDto>> GetAllProducts();
        Task<IReadOnlyList<ProductDto>> GetByPromotionId(int id);
        Task<IReadOnlyList<ProductAttributeInfoDto>> GetProductAttributes();
        Task<ProductDto> GetProductById(int id);
        Task<IReadOnlyList<ProductDto>> SearchProduct(string search);
        Task<ProductDto> UpdateProduct(int id, CreateProductDto productRequest);
    }
}
