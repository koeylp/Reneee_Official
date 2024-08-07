using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Promotion;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Reneee.Application.Services.Impl
{
    public class PromotionServiceImpl(IPromotionRepository promotionRepository,
                                      IProductPromotionRepository productPromotionRepository,
                                      IProductAttributeRepository productAttributeRepository,
                                      IUnitOfWork unitOfWork,
                                      ILogger<PromotionServiceImpl> logger,
                                      IMapper mapper) : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository = promotionRepository;
        private readonly IProductAttributeRepository _productAttributeRepository = productAttributeRepository;
        private readonly IProductPromotionRepository _productPromotionRepository = productPromotionRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<PromotionServiceImpl> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<PromotionDto> CreatePromotion(CreatePromotionDto promotionRequest)
        {
            _logger.LogInformation("Entering method CreatePromotion with body CreatePromotionDto");

            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var promotionEntity = new Promotion
                    {
                        Description = promotionRequest.Description,
                        DiscountType = Enum.Parse<DiscountType>(promotionRequest.DiscountType),
                        DiscountValue = promotionRequest.DiscountValue,
                        StartDate = DateTime.Parse(promotionRequest.StartDate),
                        EndDate = DateTime.Parse(promotionRequest.EndDate),
                    };

                    await _promotionRepository.Add(promotionEntity);

                    if (promotionRequest.ProductAttributeIds != null)
                    {
                        var productPromotionEntities = new List<ProductPromotion>();

                        foreach (var item in promotionRequest.ProductAttributeIds)
                        {
                            var existProductPromotion = await _productPromotionRepository.GetByProductAttributeIdAndStatus(item, 1);
                            if (existProductPromotion != null)
                            {
                                throw new BadRequestException($"ProductAttribute with id {item} is in another promotion.");
                            }

                            var foundProductAttribute = await _productAttributeRepository.Get(item) ?? throw new NotFoundException("Product Attribute not found with id " + item);
                            var productPromotionEntity = new ProductPromotion
                            {
                                ProductAttribute = foundProductAttribute,
                                Promotion = promotionEntity,
                                Status = 1
                            };
                            productPromotionEntities.Add(productPromotionEntity);
                        }

                        await _productPromotionRepository.AddRange(productPromotionEntities);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return _mapper.Map<PromotionDto>(promotionEntity);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating promotion");
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<PromotionDto> DisablePromotion(int id)
        {
            _logger.LogInformation($"Enter diasble promotion id {id}");
            var promotionEntity = await _promotionRepository.GetByIdAndStatus(id, 1) ?? throw new NotFoundException("Promotion not found or is not active");
            promotionEntity.Status = 0;
            await _promotionRepository.Update(promotionEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PromotionDto>(promotionEntity);
        }

        public async Task<IReadOnlyList<PromotionDto>> GetAllPromotions()
        {
            _logger.LogInformation("Fetching all Promotions");
            return _mapper.Map<IReadOnlyList<PromotionDto>>(await _promotionRepository.GetAll());
        }

        public async Task<PromotionDto> GetPromotionById(int id)
        {
            _logger.LogInformation($"Entering method get promotion by id {id}");
            return _mapper.Map<PromotionDto>(await _promotionRepository.Get(id));
        }
    }
}
