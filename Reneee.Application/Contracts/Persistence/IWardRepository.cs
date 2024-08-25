using Reneee.Domain.Entities;

namespace Reneee.Application.Contracts.Persistence
{
    public interface IWardRepository : IGenericRepository<Ward>
    {
        Task<IReadOnlyList<Ward>> GetWardsByDistrictCode(string districtCode);
    }
}
