using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;

namespace Reneee.Application.Services.CronJobs
{
    public class PromotionPriceUpdaterService(ILogger<PromotionPriceUpdaterService> logger,
                                              IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly ILogger<PromotionPriceUpdaterService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly string _cronExpression = "* * * * *";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Updating promotion statuses at {Time}", DateTimeOffset.Now);
            var cronExpression = CronExpression.Parse(_cronExpression);
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var next = cronExpression.GetNextOccurrence(now);

                if (next.HasValue)
                {
                    var delay = next.Value - now;
                    if (delay.TotalMilliseconds > 0)
                    {
                        await Task.Delay(delay, stoppingToken);
                    }

                    var shouldContinue = await UpdatePromotionStatuses(stoppingToken);

                    if (!shouldContinue)
                    {
                        _logger.LogInformation("No more promotions to update, stopping service.");
                        break;
                    }
                }
            }
        }

        private async Task<bool> UpdatePromotionStatuses(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Updating promotion statuses at {Time}", DateTimeOffset.Now);

            using (var scope = _serviceProvider.CreateScope())
            {
                var promotionRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
                var productPromotionRepository = scope.ServiceProvider.GetRequiredService<IProductPromotionRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var promotions = await promotionRepository.GetAll();

                var promotionsToUpdate = promotions.Where(p => p.StartDate <= DateTime.UtcNow && p.Status != 1).ToList();

                if (promotionsToUpdate.Count == 0)
                {
                    return false;
                }

                foreach (var promotion in promotionsToUpdate)
                {
                    promotion.Status = 1;
                    await promotionRepository.Update(promotion);

                    var productPromotions = await productPromotionRepository.GetByPromotionId(promotion.Id);

                    foreach (var productPromotion in productPromotions)
                    {
                        productPromotion.Status = 1;
                        var products = await productPromotionRepository.GetProductByPromotionId(promotion.Id);
                        foreach (var product in products)
                        {
                            product.DiscountPrice = product.OriginalPrice - promotion.DiscountValue;
                            await productRepository.Update(product);
                            foreach (var attribute in product.ProductAttributes)
                            {
                                attribute.AttributePrice = attribute.AttributePrice - promotion.DiscountValue;
                            }
                        }
                    }

                }
                await unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation("Finished updating promotion statuses at {Time}", DateTimeOffset.Now);
            return true;
        }
    }
}
