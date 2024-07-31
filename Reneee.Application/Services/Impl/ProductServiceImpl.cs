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
                                    IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductImageRepository _productImageRepository = productImageRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> CreateProduct(CreateProductDto productRequest)
        {
            var categoryEntity = await _categoryRepository.Get(productRequest.CategoryId);
            if (categoryEntity == null)
            {
                throw new NotFoundException("Category not found with id " + productRequest.CategoryId);
            }
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
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(savedProduct); 
        }
    }
}
