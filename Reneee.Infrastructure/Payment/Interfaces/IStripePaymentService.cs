namespace Reneee.Infrastructure.Payment.Interfaces
{
    public interface IStripePaymentService
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency);
        Task<string> ProcessPaymentAsync(string paymentIntentId, string paymentMethodId);
    }
}
