using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class WardRepository(ApplicationDbContext dbContext) : GenericRepository<Ward>(dbContext), IWardRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        public async Task<IReadOnlyList<Ward>> GetWardsByDistrictCode(string districtCode)
        {
            return await _dbContext.Wards
                .Where(w => w.district_code == districtCode)
                .ToListAsync();
        }
    }
}
