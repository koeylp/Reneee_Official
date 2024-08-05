using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class CommentRepository(ApplicationDbContext dbContext) : GenericRepository<Comment>(dbContext), ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
    }
}
