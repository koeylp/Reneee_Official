using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
    {
        Task AddRange(IEnumerable<OrderDetails> entities);
    }
}
