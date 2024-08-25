using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class ProvinceRepository(ApplicationDbContext dbContext) : GenericRepository<Province>(dbContext), IProvinceRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
