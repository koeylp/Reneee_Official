using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class PaymentRepository(ApplicationDbContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
