using Reneee.Application.Contracts;
using Reneee.Domain.Entities;


namespace Reneee.Persistence.Repositories
{
    public class TransactionRepository(ApplicationDbContext dbContext) : GenericRepository<Transaction>(dbContext), ITransactionRepository
    {
    }
}
