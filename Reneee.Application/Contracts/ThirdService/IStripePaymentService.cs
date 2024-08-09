namespace Reneee.Application.Contracts.ThirdService
{
    public interface IStripePaymentService
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency);
        Task<string> ProcessPaymentAsync(string paymentIntentId, string paymentMethodId);
    }
}
