using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.Payment;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services.Impl
{
    public class PaymentServiceImpl(IPaymentRepository paymentRepository,
                                    IUnitOfWork unitOfWork,
                                    ILogger<PaymentServiceImpl> logger,
                                    IMapper mapper) : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository = paymentRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;
        public async Task<PaymentDto> CreatePayment(CreatePaymentDto paymentRequest)
        {
            _logger.LogInformation("Entering method CreatePayment with body CreatePaymentDto");
            var paymentEntity = new Payment
            {
                Method = paymentRequest.Method,
                Description = paymentRequest.Description,
                Status = 1
            };
            var savedPayment = await _paymentRepository.Add(paymentEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(savedPayment);
        }
    }
}
