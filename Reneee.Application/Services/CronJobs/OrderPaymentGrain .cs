using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Reneee.Application.Services.CronJobs
{
    public interface IOrderPaymentGrain
    {
        Task SchedulePaymentCheck(int orderId);
        Task CheckAndCancelUnpaidOrder();
    }
    public class OrderPaymentGrain(IServiceProvider serviceProvider, ILogger logger) : Grain, IOrderPaymentGrain
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger _logger = logger;
        private int _orderId;

        public Task SchedulePaymentCheck(int orderId)
        {
            _orderId = orderId;

            RegisterTimer(async _ => await CheckAndCancelUnpaidOrder(), null, TimeSpan.FromHours(24), TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        public async Task CheckAndCancelUnpaidOrder()
        {
            _logger.LogInformation($"Checking payment status for order {_orderId}");

            using var scope = _serviceProvider.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

            var order = await orderService.GetOrderById(_orderId);
            if (order.Status == 0)
            {
                _logger.LogInformation($"Order {_orderId} was not paid. Canceling order.");
                await orderService.CancelOrder(_orderId);
            }
        }
    }
}
