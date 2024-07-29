using Reneee.Application.Contracts;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class AttributeValueRepository(ApplicationDbContext dbContext) : GenericRepository<AttributeValue>(dbContext), IAttributeValueRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
