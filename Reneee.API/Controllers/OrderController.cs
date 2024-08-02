using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.Order;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto orderRequest)
        {
            return Ok(await _orderService.CreateOrder(orderRequest));
        }
    }
}
