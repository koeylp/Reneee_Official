using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class PaymentRepository(ApplicationDbContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
