using Microsoft.EntityFrameworkCore;
using Reneee.Application.Contracts.Persistence;
using Reneee.Domain.Entities;

namespace Reneee.Persistence.Repositories
{
    public class DistrictRepository(ApplicationDbContext dbContext) : GenericRepository<District>(dbContext), IDisctrictRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<IReadOnlyList<District>> GetDistrictByProvinceCode(string provinceCode)
        {
            return await _dbContext.Districts
                .Where(d => d.province_code == provinceCode)
                .ToListAsync();
        }
    }
}
