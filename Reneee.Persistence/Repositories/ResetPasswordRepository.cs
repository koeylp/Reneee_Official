using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ResetPasswordRepository(ApplicationDbContext dbContext) : GenericRepository<ResetPassword>(dbContext), IResetPasswordRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
