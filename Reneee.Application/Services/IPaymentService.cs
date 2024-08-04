using Reneee.Application.DTOs.Payment;

namespace Reneee.Application.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto> CreatePayment(CreatePaymentDto paymentRequest);
        Task<string> DeletePaymentMethod(int id);
        Task<IReadOnlyList<PaymentDto>> GetAllPayments();
    }
}
