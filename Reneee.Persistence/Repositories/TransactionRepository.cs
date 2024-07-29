using Reneee.Application.Contracts;
using System.Transactions;

namespace Reneee.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
