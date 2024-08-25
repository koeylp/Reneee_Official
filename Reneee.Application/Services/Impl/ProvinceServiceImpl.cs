using AutoMapper;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.District;
using Reneee.Application.DTOs.Province;
using Reneee.Application.DTOs.Ward;

namespace Reneee.Application.Services.Impl
{
    public class ProvinceServiceImpl(IProvinceRepository provinceRepository,
                                     IDisctrictRepository disctrictRepository,
                                     IWardRepository wardRepository,
                                     IMapper mapper) : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository = provinceRepository;
        private readonly IDisctrictRepository _disctrictRepository = disctrictRepository;
        private readonly IWardRepository _wardRepository = wardRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IReadOnlyList<ProvinceDto>> GetAllProvinces()
        {
            var provinces = await _provinceRepository.GetAll();
            return _mapper.Map<IReadOnlyList<ProvinceDto>>(provinces);
        }

        public async Task<IReadOnlyList<DistrictDto>> GetDistrictsByProvince(string provinceCode)
        {
            var districts = await _disctrictRepository.GetDistrictByProvinceCode(provinceCode);
            return _mapper.Map<IReadOnlyList<DistrictDto>>(districts);
        }

        public async Task<IReadOnlyList<WardDto>> GetWardsByDistrict(string districtCode)
        {
            var wards = await _wardRepository.GetWardsByDistrictCode(districtCode);
            return _mapper.Map<IReadOnlyList<WardDto>>(wards);
        }
    }
}
