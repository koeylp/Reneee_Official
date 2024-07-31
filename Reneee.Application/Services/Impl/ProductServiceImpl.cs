using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Product;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class ProductServiceImpl(IProductRepository productRepository,
                                    IProductImageRepository productImageRepository,
                                    IUnitOfWork unitOfWork,
                                    ICategoryRepository categoryRepository,
                                    IAttributeValueRepository attributeValueRepository,
                                    IProductAttributeRepository productAttributeRepository,
                                    IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductImageRepository _productImageRepository = productImageRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IAttributeValueRepository _attributeValueRepository = attributeValueRepository;
        private readonly IProductAttributeRepository _productAttributeRepository = productAttributeRepository;  
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> CreateProduct(CreateProductDto productRequest)
        {
            var categoryEntity = await _categoryRepository.Get(productRequest.CategoryId) ?? throw new NotFoundException("Category not found with id " + productRequest.CategoryId);
            var productEntity = new Product
            {
                Name = productRequest.Name,
                Thumbnail = productRequest.Thumbnail,
                OriginalPrice = productRequest.OriginalPrice,
                DiscountPrice = productRequest.OriginalPrice,
                Description = productRequest.Description,
                AdditionalInfo = productRequest.AdditionalInfo,
                Ingredients = productRequest.Ingredients,
                Guideline = productRequest.Guideline,
                CreatedAt = DateTime.UtcNow,
                Category = categoryEntity,
                Status = 0
            };
            Product savedProduct = await _productRepository.Add(productEntity);
            foreach (var item in productRequest.ProductImages)
            {
                var image = new ProductImage
                {
                    Url = item,
                    Product = savedProduct,
                    Status = 1
                };
                await _productImageRepository.Add(image);
            }
            foreach (var item in productRequest.ProductAttributeValues)
            {
                var attributeValueEntity = await _attributeValueRepository.Get(item.attibuteValueId)
                    ?? throw new NotFoundException("Attribute value not found when creating product attribtue");
                var attributeValue = new ProductAttribute
                {
                    Product = savedProduct,
                    AttributeValue = attributeValueEntity,
                    AttributePrice = item.attributePrice,
                    Stock = item.stock,
                    Status = 0
                };
                await _productAttributeRepository.Add(attributeValue);
            }
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(savedProduct);
        }

        public async Task<string> DeleteProduct(int id)
        {
            var productEntity = await _productRepository.Get(id) ?? throw new NotFoundException($"Product not fount with {id}");
            if (productEntity.Status == 1) throw new BadRequestException($"Cannot delete product with id - {id} which is enabled");
            await _productRepository.Delete(productEntity);
            await _unitOfWork.SaveChangesAsync();
            return "Done Deleting";
        }

        public async Task<ProductDto> DisableProduct(int id)
        {
            var productEntity = await CallAndSetStatusProduct(id, 0);
            await _productRepository.Update(productEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<ProductDto> EnableProduct(int id)
        {
            var productEntity = await CallAndSetStatusProduct(id, 1);
            await _productRepository.Update(productEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<IReadOnlyList<ProductDto>> FilterProduct(decimal? filter_v_price_gte, decimal? filter_v_price_lte,
                                                            string? sort_by, int? filter_v_availability)
        {
            return _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository
                    .GetFilteredProducts(filter_v_price_gte, filter_v_price_lte, sort_by, filter_v_availability));
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProducts()
        {
            return _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository.GetAll());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var productEntity = await _productRepository.Get(id) ?? throw new NotFoundException($"Product not fount with {id}");
            return _mapper.Map<ProductDto>(productEntity);
        }

        private async Task<Product> CallAndSetStatusProduct(int id, int status)
        {
            var productEntity = await _productRepository.Get(id) ?? throw new NotFoundException($"Product not fount with {id}");
            switch (status)
            {
                case 0:
                    if (productEntity.Status == 0) throw new BadRequestException("Product was disabled!");
                    break;
                case 1:
                    if (productEntity.Status == 1) throw new BadRequestException("Product was enabled!");
                    break;
            }
            productEntity.Status = status;
            return productEntity;
        }
    }
}
