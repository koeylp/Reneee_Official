using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class AttributeValueRepository(ApplicationDbContext dbContext) : GenericRepository<AttributeValue>(dbContext), IAttributeValueRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
