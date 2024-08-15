using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IReadOnlyList<Transaction>> GetTransactionByUser(User user);
        Task<Transaction> GetTransactionByOrder(Order order);
    }
}
