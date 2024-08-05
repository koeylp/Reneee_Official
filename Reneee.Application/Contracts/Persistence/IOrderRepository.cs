using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IReadOnlyList<Order>> GetOrderByUser(User user);
    }
}
