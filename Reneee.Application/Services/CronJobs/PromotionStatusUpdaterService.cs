using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;

namespace Reneee.Application.Services.CronJobs
{
    public class PromotionStatusUpdaterService(ILogger<PromotionStatusUpdaterService> logger, 
                                               IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly string _cronExpression = "* * * * *"; 

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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

                    await UpdatePromotionStatuses(stoppingToken);
                }
            }
        }

        private async Task UpdatePromotionStatuses(CancellationToken stoppingToken)
        {
            logger.LogInformation("Updating promotion statuses at {Time}", DateTimeOffset.Now);

            using (var scope = serviceProvider.CreateScope())
            {
                var promotionRepository = scope.ServiceProvider.GetRequiredService<IPromotionRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var promotions = await promotionRepository.GetAll();

                foreach (var promotion in promotions)
                {
                    if (promotion.StartDate <= DateTime.UtcNow && promotion.Status != 1)
                    {
                        promotion.Status = 1;
                        await promotionRepository.Update(promotion);
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }

            logger.LogInformation("Finished updating promotion statuses at {Time}", DateTimeOffset.Now);
        }
    }
}
