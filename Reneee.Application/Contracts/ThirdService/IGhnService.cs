using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Ward;

namespace Reneee.Application.Contracts.ThirdService
{
    public interface IGhnService
    {
        Task<List<DistrictDto>> GetDisctrictByPronvinceId(int pronvinceId);
        Task<List<ProvinceDto>> GetProvinces();
        Task<List<WardDto>> GetWardByDistrictId(int districtId);
    }
}
