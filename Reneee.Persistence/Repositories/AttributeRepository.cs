using Reneee.Application.Contracts.Persistence;
using Attribute = Reneee.Domain.Entities.Attribute;

namespace Reneee.Persistence.Repositories
{
    public class AttributeRepository(ApplicationDbContext dbContext) : GenericRepository<Attribute>(dbContext), IAttributeRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
