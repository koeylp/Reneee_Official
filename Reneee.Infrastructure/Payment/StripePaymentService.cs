using Microsoft.Extensions.Options;
using Reneee.Application.Contracts.ThirdService;
using Stripe;

namespace Reneee.Infrastructure.Payment
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly StripeSettings _stripeSettings;

        public StripePaymentService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.ApiKey;
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), 
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }

        public async Task<string> ProcessPaymentAsync(string paymentIntentId, string paymentMethodId)
        {
            var options = new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethodId,
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.ConfirmAsync(paymentIntentId, options);
            return paymentIntent.Status;
        }
    }
}
