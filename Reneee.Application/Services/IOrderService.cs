using Reneee.Application.DTOs.Order;

namespace Reneee.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrder(CreateOrderDto orderRequest);
    }
}
