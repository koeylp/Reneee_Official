using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;

namespace Reneee.Application.Services.CronJobs
{
    public class PromotionPriceUpdaterService : IHostedService, IDisposable
    {
        private readonly ILogger<PromotionPriceUpdaterService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private readonly string _cronExpression = "* * * * *";
        private CronExpression _cronSchedule;
        private DateTime _nextRun;
        private const string PERCENTAGE = "Percentage";
        private const string FIXED_AMOUNT = "FixedAmount";

        public PromotionPriceUpdaterService(ILogger<PromotionPriceUpdaterService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _cronSchedule = CronExpression.Parse(_cronExpression);
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

            using (var scope = _serviceProvider.CreateScope())
            {
                var promotionRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
                var productPromotionRepository = scope.ServiceProvider.GetRequiredService<IProductPromotionRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var productAttributeRepository = scope.ServiceProvider.GetRequiredService<IProductAttributeRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var promotions = await promotionRepository.GetAll();

                var promotionsToUpdate = promotions.Where(p => p.StartDate <= DateTime.UtcNow && p.Status != 1).ToList();

                if (promotionsToUpdate.Count == 0)
                {
                    _logger.LogInformation("No promotions to update at {Time}", DateTimeOffset.Now);
                    return;
                }

                foreach (var promotion in promotionsToUpdate)
                {
                    promotion.Status = 1;
                    await promotionRepository.Update(promotion);

                    var productPromotions = await productPromotionRepository.GetByPromotionId(promotion.Id);

                    foreach (var productPromotion in productPromotions)
                    {
                        var productAttribute = productPromotion.ProductAttribute;
                        switch (promotion.DiscountType.ToString())
                        {
                            case PERCENTAGE:
                                productAttribute.AttributeDiscountPrice = productAttribute.AttributePrice * (1 - promotion.DiscountValue);
                                break;
                            case FIXED_AMOUNT:
                                productAttribute.AttributeDiscountPrice = productAttribute.AttributePrice - promotion.DiscountValue;
                                break;
                        }
                        await productAttributeRepository.Update(productAttribute);
                        productPromotion.Status = 1;
                        var products = await productPromotionRepository.GetProductByPromotionId(promotion.Id);
                        foreach (var product in products)
                        {
                            switch (promotion.DiscountType.ToString())
                            {
                                case PERCENTAGE:
                                    product.DiscountPrice = product.OriginalPrice * (1 - promotion.DiscountValue);
                                    break;
                                case FIXED_AMOUNT:
                                    product.DiscountPrice = product.OriginalPrice - promotion.DiscountValue;
                                    break;
                            }
                            await productRepository.Update(product);
                        }
                    }
                }
                await unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation("Finished updating promotion statuses at {Time}", DateTimeOffset.Now);
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
