using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Payment;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment([FromBody] CreatePaymentDto paymentRequest)
        {
            return Ok(await _paymentService.CreatePayment(paymentRequest));
        }
    }
}
