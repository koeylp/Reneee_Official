using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Transaction> GetTransactionByOrder(Order order)
        {
            return await _dbContext.Transactions
                 .FirstOrDefaultAsync(t => t.Order == order);

        }

        public async Task<IReadOnlyList<Transaction>> GetTransactionByUser(User user)
        {
            return await _dbContext.Transactions
                .Where(t => t.User == user)
                .ToListAsync();
        }
    }
}
