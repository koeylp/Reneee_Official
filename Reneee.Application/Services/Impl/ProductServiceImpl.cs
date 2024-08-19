using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Product;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;
using System.Collections.Generic;

namespace Reneee.Application.Services.Impl
{
    public class ProductServiceImpl(IProductRepository productRepository,
                                    IProductImageRepository productImageRepository,
                                    IUnitOfWork unitOfWork,
                                    ICategoryRepository categoryRepository,
                                    IAttributeValueRepository attributeValueRepository,
                                    IProductAttributeRepository productAttributeRepository,
                                    IProductPromotionRepository productPromotionRepository,
                                    ILogger<ProductServiceImpl> logger,
                                    IMapper mapper) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IProductImageRepository _productImageRepository = productImageRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IAttributeValueRepository _attributeValueRepository = attributeValueRepository;
        private readonly IProductAttributeRepository _productAttributeRepository = productAttributeRepository;
        private readonly IProductPromotionRepository _productPromotionRepository = productPromotionRepository;
        private readonly ILogger<ProductServiceImpl> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<ProductDto> CreateProduct(CreateProductDto productRequest)
        {
            _logger.LogInformation("Creating product with name {ProductName}", productRequest.Name);
            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var categoryEntity = await _categoryRepository.GetCategoryByIdAndStatus(productRequest.CategoryId, 1)
                                        ?? throw new NotFoundException("Category not found with id " + productRequest.CategoryId);

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
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Category = categoryEntity,
                        Status = 0
                    };

                    Product savedProduct = await _productRepository.Add(productEntity);
                    if (productRequest.ProductImages != null)
                    {
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
                    }
                    int totalQuantity = 0;
                    var attributeValueInput = productRequest.ProductAttributeValues;
                    if (attributeValueInput == null || attributeValueInput.Length == 0) throw new BadRequestException("AttributeValue is required!");

                    foreach (var item in attributeValueInput)
                    {
                        var attributeValueEntity = await _attributeValueRepository.Get(item.attributeValueId)
                            ?? throw new NotFoundException("Attribute value not found when creating product attribute");
                        if (item.stock < 0)
                        {
                            throw new ArgumentException("Stock cannot be negative.");
                        }
                        if (item.stock == 0) throw new ArgumentException("Stock is required");
                        if (item.attributePrice < 0)
                        {
                            throw new ArgumentException("AttributePrice cannot be negative.");
                        }

                        var productAttributeEntity = new ProductAttribute
                        {
                            Product = savedProduct,
                            AttributeValue = attributeValueEntity,
                            AttributePrice = item.attributePrice,
                            AttributeDiscountPrice = item.attributePrice,
                            Stock = item.stock,
                            Status = 0
                        };
                        await _productAttributeRepository.Add(productAttributeEntity);
                        totalQuantity += item.stock;
                    }

                    savedProduct.TotalQuantity = totalQuantity;
                    await _productRepository.Update(savedProduct);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();
                    _logger.LogInformation("Product created with ID {ProductId}", savedProduct.Id);
                    return _mapper.Map<ProductDto>(savedProduct);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating product");
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<string> DeleteProduct(int id)
        {
            _logger.LogInformation("Entering method DeleteProduct with parameter {Id}", id);
            var productEntity = await _productRepository.Get(id);
            if (productEntity == null)
            {
                var message = $"Product with Id {id} not found";
                throw new NotFoundException(message);
            }
            if (productEntity.Status == 1)
            {
                var message = $"Cannot delete product with id - {id} which is enabled";
                throw new BadRequestException(message);
            }
            await _productRepository.Delete(productEntity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product with ID {Id} deleted", id);
            return "Done Deleting";
        }

        public async Task<ProductDto> DisableProduct(int id)
        {
            _logger.LogInformation("Disabling product with ID {Id}", id);
            var productEntity = await CallAndSetStatusProduct(id, 0);
            await _productRepository.Update(productEntity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product with ID {Id} disabled", id);
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<ProductDto> EnableProduct(int id)
        {
            _logger.LogInformation("Enabling product with ID {Id}", id);
            var productEntity = await CallAndSetStatusProduct(id, 1);
            await _productRepository.Update(productEntity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product with ID {Id} enabled", id);
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<IReadOnlyList<ProductDto>> FilterProduct(decimal? filter_v_price_gte, decimal? filter_v_price_lte,
                                                            string? sort_by, int? filter_v_availability, string? categories)
        {
            _logger.LogInformation("Filtering products with price range {PriceGte} - {PriceLte}, sort by {SortBy}, availability {Availability}",
                filter_v_price_gte, filter_v_price_lte, sort_by, filter_v_availability);

            return _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository
                    .GetFilteredProducts(filter_v_price_gte, filter_v_price_lte, sort_by, filter_v_availability, categories));
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProducts()
        {
            _logger.LogInformation("Fetching all products");
            return _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository.GetAll());
        }

        public async Task<IReadOnlyList<ProductDto>> GetByPromotionId(int id)
        {
            var productList = await _productPromotionRepository.GetProductByPromotionId(id);
            return _mapper.Map<IReadOnlyList<ProductDto>>(productList);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            _logger.LogInformation("Fetching product with ID {Id}", id);
            var productEntity = await _productRepository.Get(id) ?? throw new NotFoundException($"Product not found with ID {id}");
            return _mapper.Map<ProductDto>(productEntity);
        }

        public async Task<IReadOnlyList<ProductDto>> SearchProduct(string search)
        {
            return _mapper.Map<IReadOnlyList<ProductDto>>(await _productRepository.Search(search));
        }

        public async Task<ProductDto> UpdateProduct(int productId, CreateProductDto productRequest)
        {
            _logger.LogInformation("Updating product with ID {ProductId}", productId);
            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var productEntity = await _productRepository.Get(productId)
                                        ?? throw new NotFoundException("Product not found with id " + productId);

                    var categoryEntity = await _categoryRepository.GetCategoryByIdAndStatus(productRequest.CategoryId, 1)
                                        ?? throw new NotFoundException("Category not found with id " + productRequest.CategoryId);

                    productEntity.Name = productRequest.Name;
                    productEntity.Thumbnail = productRequest.Thumbnail;
                    productEntity.OriginalPrice = productRequest.OriginalPrice;
                    productEntity.DiscountPrice = productRequest.OriginalPrice;
                    productEntity.Description = productRequest.Description;
                    productEntity.AdditionalInfo = productRequest.AdditionalInfo;
                    productEntity.Ingredients = productRequest.Ingredients;
                    productEntity.Guideline = productRequest.Guideline;
                    productEntity.Category = categoryEntity;
                    productEntity.UpdatedAt = DateTime.Now;

                    if (productRequest.ProductImages != null)
                    {
                        var existingImages = await _productImageRepository.GetByProductId(productId);
                        _productImageRepository.RemoveRange(existingImages);

                        var newImages = productRequest.ProductImages.Select(imageUrl => new ProductImage
                        {
                            Url = imageUrl,
                            Product = productEntity,
                            Status = 1
                        }).ToList();

                        await _productImageRepository.AddRange(newImages);
                    }

                    int totalQuantity = 0;
                    if (productRequest.ProductAttributeValues != null)
                    {
                        var existingAttributes = await _productAttributeRepository.GetByProductId(productId);
                        _productAttributeRepository.RemoveRange(existingAttributes);

                        foreach (var item in productRequest.ProductAttributeValues)
                        {
                            var attributeValueEntity = await _attributeValueRepository.Get(item.attributeValueId)
                                ?? throw new NotFoundException("Attribute value not found with id " + item.attributeValueId);

                            var productAttributeEntity = new ProductAttribute
                            {
                                Product = productEntity,
                                AttributeValue = attributeValueEntity,
                                AttributePrice = item.attributePrice,
                                Stock = item.stock,
                                Status = 0
                            };

                            await _productAttributeRepository.Add(productAttributeEntity);
                            totalQuantity += item.stock;
                        }
                    }

                    productEntity.TotalQuantity = totalQuantity;

                    await _productRepository.Update(productEntity);
                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();

                    _logger.LogInformation("Product updated with ID {ProductId}", productEntity.Id);
                    return _mapper.Map<ProductDto>(productEntity);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating product");
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }


        private async Task<Product> CallAndSetStatusProduct(int id, int status)
        {
            _logger.LogInformation("Setting status for product with ID {Id} to {Status}", id, status);
            var productEntity = await _productRepository.Get(id) ?? throw new NotFoundException($"Product not found with ID {id}");

            switch (status)
            {
                case 0:
                    if (productEntity.Status == 0) throw new BadRequestException("Product is already disabled!");
                    break;
                case 1:
                    if (productEntity.Status == 1) throw new BadRequestException("Product is already enabled!");
                    break;
            }

            productEntity.Status = status;
            return productEntity;
        }
    }
}
