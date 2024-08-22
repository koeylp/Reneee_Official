using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Constants;
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
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER)]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto orderRequest)
        {
            return Ok(await _orderService.CreateOrder(orderRequest));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] int id)
        {
            return Ok(await _orderService.GetOrderById(id));
        }

        [HttpGet("staff")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER)]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersByUser([FromQuery] int status)
        {
            return Ok(await _orderService.GetOrdersByUser(status));
        }

        [HttpPut("status/update")]
        public async Task<ActionResult<OrderDto>> UpdateOrderStatus([FromQuery] int id, int status)
        {
            return Ok(await _orderService.UpdateOrderStatus(id, status));
        }

        [HttpPut("cancel/{id}")]
        [Authorize(Roles = RoleConstants.ROLE_CUSTOMER)]
        public async Task<ActionResult<OrderDto>> CancelOrder([FromRoute] int id)
        {
            return Ok(await _orderService.CancelOrder(id));
        }
    }
}
