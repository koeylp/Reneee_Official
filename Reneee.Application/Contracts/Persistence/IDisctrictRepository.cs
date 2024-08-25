using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IDisctrictRepository : IGenericRepository<District>
    {
        Task<IReadOnlyList<District>> GetDistrictByProvinceCode(string provinceCode);
    }
}
