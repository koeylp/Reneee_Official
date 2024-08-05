﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersByUser()
        {
            return Ok(await _orderService.GetOrdersByUser());
        }
    }
}
