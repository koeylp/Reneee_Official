using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Payment;
using Reneee.Application.Services;
using Reneee.Infrastructure.Payment.Interfaces;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService,
                                   IStripePaymentService stripePaymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;
        private readonly IStripePaymentService _stripePaymentService = stripePaymentService;

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentDto paymentRequest)
        {
            return Ok(await _paymentService.CreatePayment(paymentRequest));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PaymentDto>>> GetAllPayments()
        {
            return Ok(await _paymentService.GetAllPayments());
        }

        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeletePaymentMethod(int id)
        {
            return Ok(await _paymentService.DeletePaymentMethod(id));
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent(decimal amount, string currency)
        {
            var clientSecret = await _stripePaymentService.CreatePaymentIntentAsync(amount, currency);
            return Ok(new { clientSecret });
        }

        [HttpPost("process-payment")]
        public async Task<IActionResult> ProcessPayment(string paymentIntentId, string paymentMethodId)
        {
            var status = await _stripePaymentService.ProcessPaymentAsync(paymentIntentId, paymentMethodId);
            return Ok(new { status });
        }
    }
}
