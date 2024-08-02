using Reneee.Application.DTOs.Payment;

namespace Reneee.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreatePayment(CreatePaymentDto paymentRequest);
    }
}
