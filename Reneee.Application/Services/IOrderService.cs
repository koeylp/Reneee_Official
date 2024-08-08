using Reneee.Application.DTOs.Order;

namespace Reneee.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CancelOrder(int id);
        Task<OrderDto> CreateOrder(CreateOrderDto orderRequest);
        Task<IReadOnlyList<OrderDto>> GetAllOrders();
        Task<OrderDto> GetOrderById(int id);
        Task<IReadOnlyList<OrderDto>> GetOrdersByUser();
        Task<OrderDto> UpdateOrderStatus(int id, int status);
    }
}
