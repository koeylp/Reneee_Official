
using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Ward;

namespace Reneee.Application.Services
{
    public interface IProvinceService
    {
        Task<IReadOnlyList<ProvinceDto>> GetAllProvinces();
        Task<IReadOnlyList<DistrictDto>> GetDistrictsByProvince(string provinceCode);
        Task<IReadOnlyList<WardDto>> GetWardsByDistrict(string districtCode);
    }
}
