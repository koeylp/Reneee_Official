using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;


namespace Reneee.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionRepository
    {
    }
}
