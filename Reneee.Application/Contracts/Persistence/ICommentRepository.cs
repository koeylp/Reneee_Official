using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IReadOnlyList<Comment>> GetCommentByProduct(Product product);
    }
}
