using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.CronJobs
{
    public class PromotionPriceUpdaterService : IHostedService, IDisposable
    {
        private readonly ILogger<PromotionPriceUpdaterService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private readonly CronExpression _cronSchedule;
        private DateTime _nextRun;

        private const string PERCENTAGE = "Percentage";
        private const string FIXED_AMOUNT = "FixedAmount";
        private const int ACTIVE_STATUS = 1;
        private const int INACTIVE_STATUS = 0;

        public PromotionPriceUpdaterService(ILogger<PromotionPriceUpdaterService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _cronSchedule = CronExpression.Parse("* * * * *");
            _nextRun = _cronSchedule.GetNextOccurrence(DateTime.UtcNow) ?? DateTime.MinValue;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Promotion Price Updater Service running.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var now = DateTime.UtcNow;
            if (now >= _nextRun)
            {
                _nextRun = _cronSchedule.GetNextOccurrence(DateTime.UtcNow) ?? DateTime.MinValue;
                await UpdatePromotionStatuses(CancellationToken.None);
            }
        }

        private async Task UpdatePromotionStatuses(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Updating promotion statuses at {Time}", DateTimeOffset.Now);

            using var scope = _serviceProvider.CreateScope();
            var promotionRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
            var productPromotionRepository = scope.ServiceProvider.GetRequiredService<IProductPromotionRepository>();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            var productAttributeRepository = scope.ServiceProvider.GetRequiredService<IProductAttributeRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var promotions = await promotionRepository.GetAll();
            var outdatedPromotions = promotions.Where(p => p.EndDate < DateTime.Now && p.Status == ACTIVE_STATUS).ToList();
            var promotionsToUpdate = promotions.Where(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now && p.Status != ACTIVE_STATUS).ToList();
            await ProcessOutdatedPromotions(outdatedPromotions, promotionRepository, productPromotionRepository, productRepository, productAttributeRepository);
            await ProcessActivePromotions(promotionsToUpdate, promotionRepository, productPromotionRepository, productRepository, productAttributeRepository);

            await unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Finished updating promotion statuses at {Time}", DateTimeOffset.Now);
        }

        private async Task ProcessOutdatedPromotions(IEnumerable<Promotion> promotions,
                                                     IPromotionRepository promotionRepository,
                                                     IProductPromotionRepository productPromotionRepository,
                                                     IProductRepository productRepository,
                                                     IProductAttributeRepository productAttributeRepository)
        {
            foreach (var promotion in promotions)
            {
                promotion.Status = INACTIVE_STATUS;
                await promotionRepository.Update(promotion);

                var productPromotions = await productPromotionRepository.GetByPromotionId(promotion.Id);
                foreach (var productPromotion in productPromotions)
                {
                    var productAttribute = productPromotion.ProductAttribute;
                    productAttribute.AttributeDiscountPrice = productAttribute.AttributePrice;
                    await productAttributeRepository.Update(productAttribute);

                    productPromotion.Status = INACTIVE_STATUS;
                    var products = await productPromotionRepository.GetProductByPromotionId(promotion.Id);
                    foreach (var product in products)
                    {
                        product.DiscountPrice = product.OriginalPrice;
                        await productRepository.Update(product);
                    }
                }
            }
        }

        private async Task ProcessActivePromotions(IEnumerable<Promotion> promotions,
                                                   IPromotionRepository promotionRepository,
                                                   IProductPromotionRepository productPromotionRepository,
                                                   IProductRepository productRepository,
                                                   IProductAttributeRepository productAttributeRepository)
        {
            if (!promotions.Any())
            {
                _logger.LogInformation("No promotions to update at {Time}", DateTimeOffset.Now);
                return;
            }

            foreach (var promotion in promotions)
            {
                promotion.Status = ACTIVE_STATUS;
                await promotionRepository.Update(promotion);

                var productPromotions = await productPromotionRepository.GetByPromotionId(promotion.Id);
                foreach (var productPromotion in productPromotions)
                {
                    var productAttribute = productPromotion.ProductAttribute;
                    productAttribute.AttributeDiscountPrice = promotion.DiscountType.ToString() switch
                    {
                        PERCENTAGE => productAttribute.AttributePrice * (1 - promotion.DiscountValue / 100),
                        FIXED_AMOUNT => productAttribute.AttributePrice - promotion.DiscountValue,
                        _ => productAttribute.AttributePrice
                    };
                    await productAttributeRepository.Update(productAttribute);

                    productPromotion.Status = ACTIVE_STATUS;
                    var products = await productPromotionRepository.GetProductByPromotionId(promotion.Id);
                    foreach (var product in products)
                    {
                        product.DiscountPrice = promotion.DiscountType.ToString() switch
                        {
                            PERCENTAGE => product.OriginalPrice * (1 - promotion.DiscountValue / 100),
                            FIXED_AMOUNT => product.OriginalPrice - promotion.DiscountValue,
                            _ => product.OriginalPrice
                        };
                        await productRepository.Update(product);
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Promotion Price Updater Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
